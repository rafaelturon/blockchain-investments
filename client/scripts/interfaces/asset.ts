'use strict';

module kmpoint {
   export interface IAsset {
        name: string;
        type: string;
        value: number;
        register(): void;
    } 
}
module kmpoint {
    export interface ISubscriber{
        isSubscriber: boolean
    }
}

module kmpoint {
    export class Asset implements IAsset, ISubscriber {
        constructor(public name:string, public type: string, public value: number, public isSubscriber: boolean){}
        register(): void {
            console.log('Ativo cadastrado com sucesso!');
        }
    }
}