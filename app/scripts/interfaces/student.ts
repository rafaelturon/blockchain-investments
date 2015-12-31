'use strict';

module kmpoint {
   export interface IStudent {
        name: string;
        email: string;
        idade?: number;
        register(): void;
    } 
}
module kmpoint {
    export interface ISubscriber{
        isSubscriber: boolean
}
}

module kmpoint {
    export class Student implements IStudent, ISubscriber {
        constructor(public name:string, public email: string, public isSubscriber: boolean, public age?: number){}
        register(): void {
            console.log('Aluno cadastrado com sucesso!');
        }
    }
}