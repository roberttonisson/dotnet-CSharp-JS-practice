import { StringifyOptions } from "querystring";
import { ITopping } from "./ITopping";
import { IPizzaType } from "./IPizzaType";

export interface IDefaultTopping{
    id: string;
    toppingId: string;
    topping: ITopping | null;

    pizzaTypeId: string;
    pizzaType: IPizzaType | null;
}

export interface IDefaultToppingCreate {
    id: string | undefined;
    toppingId: string;

    pizzaTypeId: string;
}

