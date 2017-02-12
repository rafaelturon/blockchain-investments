import { Component } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { DomSanitizer } from '@angular/platform-browser';

declare var Ledger: any;

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    public bitid: BitId;
    public sanitizer: DomSanitizer;

    constructor(http: Http, sanitizer:DomSanitizer) {
        http.get('/api/identity').subscribe(result => {
            this.bitid = result.json() as BitId;
        });
        this.sanitizer = sanitizer;
    }

    getTrustedUri() {
        return this.sanitizer.bypassSecurityTrustUrl(this.bitid.bitIdUri);
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
