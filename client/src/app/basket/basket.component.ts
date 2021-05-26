import { Component, IterableDiffers, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasKetItem } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$! : Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
  }

  //remove item
  removeBasketItem(item: IBasKetItem){
    this.basketService.removeItemFromBasket(item);
  }

  //increment item
  incrementItemQuantity(item: IBasKetItem){
    this.basketService.incrementItemQuantity(item);
  }

  decrementItemQuantity(item: IBasKetItem){
    this.basketService.decrementItemQuantity(item);
  }

}
