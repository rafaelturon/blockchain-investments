import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'securities',
    templateUrl: './securities.component.html'
})
export class SecuritiesComponent {
    public securities: Security[];

    constructor(http: Http) {
        http.get('/api/security').subscribe(result => {
            this.securities = result.json() as Security[];
        });
    }
}

interface Security {
    title: string;
    exchange: string;
    country: string;
    ticker: string;
}
