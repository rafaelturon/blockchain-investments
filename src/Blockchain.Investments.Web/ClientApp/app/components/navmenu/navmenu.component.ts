import { Component } from '@angular/core';
declare var _abcUi: any;
declare var _account: any;

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})

export class NavMenuComponent {
    login(event) {
        if (_account === null) {
            _abcUi.openLoginWindow(function(error, account) {
                _account = account;
            });
        } else {
            _account.logout();
        }
        // Account Id: _account.repoInfo.dataKey.toString('base64')
    }
}
