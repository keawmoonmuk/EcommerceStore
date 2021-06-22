import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket, IBasKetItem } from '../../models/basket';
import { IOrderItem } from '../../models/order';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {

  basket$ : Observable<IBasket>;
  @Output() decrement: EventEmitter<IBasKetItem> = new EventEmitter<IBasKetItem>();
  @Output() increment: EventEmitter<IBasKetItem> = new EventEmitter<IBasKetItem>();
  @Output() remove: EventEmitter<IBasKetItem> = new EventEmitter<IBasKetItem>();
  @Input() isBasket =true;
  @Input() items: IBasKetItem[] | IOrderItem[] = [];
  @Input() isOrder = false;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
  }

  decrementItemQuantity(item: IBasKetItem) {
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: IBasKetItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: IBasKetItem) {
    this.remove.emit(item);
  }
    //remove item
    // removeBasketItem(item: IBasKetItem){
    //   this.basketService.removeItemFromBasket(item);
    // }
  
    //increment item
    // incrementItemQuantity(item: IBasKetItem){
    //   this.basketService.incrementItemQuantity(item);
    // }
  
    // decrementItemQuantity(item: IBasKetItem){
    //   this.basketService.decrementItemQuantity(item);
    // }

}
