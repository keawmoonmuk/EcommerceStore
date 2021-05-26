// import uuid from 'uuid/v4';
import { v4 as uuidv4} from 'uuid';

export interface IBasket {
    id?: string;
    items?:IBasKetItem[];
}

export interface IBasKetItem {
    id?: number;
    productName?: string;
    price?: number;
    quantity: number;
    pictureUrl?: string;
    brand?: string;
    type?: string;
}

export class Basket implements IBasket {
    id = uuidv4();
    items: IBasKetItem[] = [];
}
export interface IBasketTotal {
    shipping: number;
    subtotal: number;
    total:number;
}