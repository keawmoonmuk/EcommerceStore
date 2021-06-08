import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasKetItem, IBasketTotal } from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null!);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotal>(null!);   //total 
  basketTotal$ = this.basketTotalSource.asObservable();
  shipping =0;

  constructor(private http: HttpClient) { }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    this.calculateTotals();
  }  

  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
    .pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.calculateTotals();       //calcurate
        // console.log(this.getCurrentBasketValue());
      })
    );
  }

  //set basket
  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotals();   //calculate
      // console.log(response);
    }, error => {
      console.log(error);
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  //add item basket
  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasKetItem = this.mapProductItemToBasketItem(item, quantity);
    // const basket = this.getCurrentBasketValue() ?? this.createBasket();
    let basket = this.getCurrentBasketValue();
    if(basket === null){
      basket = this.createBasket();
    }
    basket.items = this.addOrUpdateItem(basket.items!, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasKetItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items!.findIndex(x => x.id === item.id);
    basket.items![foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasKetItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items!.findIndex(x => x.id === item.id);
    if(basket.items![foundItemIndex].quantity > 1)
    {
      basket.items![foundItemIndex].quantity--;
      this.setBasket(basket);
    }else{
      this.removeItemFromBasket(item);
    }
  }

  //remove item
  removeItemFromBasket(item: IBasKetItem) {
    const basket = this.getCurrentBasketValue();
    if(basket.items!.some(x => x.id ===item.id)){
      basket.items = basket.items!.filter(i => i.id !== item.id)
      if(basket.items.length > 0){
        this.setBasket(basket);
      }else{
        this.deleteBasket(basket);
      }
    }
  }

  //delete local
  deleteLocalBasket(id: string) {
    this.basketSource.next(null!);
    this.basketTotalSource.next(null!);
    localStorage.removeItem('basket_id');
  }

  //delete
  deleteBasket(basket: IBasket) {
   return this.http.delete(this.baseUrl +'basket?id=' + basket.id).subscribe(() => {
     this.basketSource.next(null!);
     this.basketTotalSource.next(null!);
     localStorage.removeItem('basket_id');
   }, error => {
     console.log(error);
   });
  }

  //calculate
  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subtotal = basket.items!.reduce((a, b) => (b.price! * b.quantity!) + a , 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping, total,subtotal});

  }

  //add or update order item
 private addOrUpdateItem(items: IBasKetItem[] , itemToAdd: IBasKetItem, quantity: number): 
  IBasKetItem[]  {

    const index = items.findIndex(i => i.id === itemToAdd.id);
    if(index === -1)  {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }else{

      items[index].quantity! += quantity;
    }

    return items;
   
  }

  //create basket
  private  createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);

    return basket;
  }

  //map product item
 private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasKetItem {
    return {
      id: item.id,
      productName:item.name,
      price: item.price,
      pictureUrl:item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type:item.productType
    };
  }

}
