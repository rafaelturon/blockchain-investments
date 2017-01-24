import { Component } from '@angular/core';
declare var Ledger: any;

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent {
    login(event) {
        Ledger.init({ callback: this.callback });
        Ledger.bitid('bitid://bitid.bitcoin.blue/callback?x=5f38d0fb45b25015&u=1');//
    }
    callback(event) {
        if (event.response.command == "bitid") {
            console.log(event.response);
        }
    }
}
