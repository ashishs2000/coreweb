import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent }  from './app.component';
import { EmployeeComponent }  from './employee/component';

@NgModule({
  imports:      [ 
    BrowserModule,
    HttpModule,
    RouterModule.forRoot([
      { path: 'app', component: AppComponent },
      { path: 'employee', component: EmployeeComponent },
      { path: '', redirectTo: 'app', pathMatch: 'full' },
      { path: '**', redirectTo: 'app', pathMatch: 'full' }
    ]),
  ],
  declarations: [ 
    AppComponent,
    EmployeeComponent
  ],
  bootstrap:    [ AppComponent ]
})
export class AppModule {
 }

