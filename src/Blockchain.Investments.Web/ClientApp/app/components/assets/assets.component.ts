import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'assets',
    templateUrl: './assets.component.html'
})
export class AssetsComponent {
    public assets: Asset[];

    constructor(http: Http) {
        http.get('/api/assets').subscribe(result => {
            this.assets = result.json() as Asset[];
        });
    }
}

interface Asset {
    name: string;
    value: number;
    date: Date;
    percentage: number;
}
