import { Component, IterableDiffers, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasKetItem, IBasketTotals } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$ : Observable<IBasket>;
  basketTotals$: Observable<IBasketTotals>;
  
  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
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
