import { Component } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

declare var Ledger: any;

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    public bitid: BitId;

    constructor(http: Http) {
        http.get('/api/identity').subscribe(result => {
            this.bitid = result.json() as BitId;
        });
    }

    ledgerLogin(event) {
        Ledger.init({ callback: this.callback });
        Ledger.bitid(this.bitid.bitIdUri);
    }

    manualLogin(event) {
        
    }

    callback(event) {
        if (event.response.command == "bitid") {
            console.log(event.response);
        }
    }
}

interface BitId {
    bitIdUri: string;
    bitIdImageQr: string;
}
