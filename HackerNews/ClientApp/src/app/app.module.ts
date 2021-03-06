import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NewsComponent } from "./news/news.component";
import { NewsSearchComponent } from "./news/news-search/news-search.component";

import { HomeMenuComponent } from "./home-menu/home-menu.component";
import { NullTransformPipe } from './Pipes/null-transform.pipe';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NewsComponent,
    NewsSearchComponent,
    HomeMenuComponent,
    NullTransformPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: 'home', component: HomeMenuComponent},
      { path: 'news', component: NewsComponent},
      { path: 'news-search', component: NewsSearchComponent}
    ]),
    NgxPaginationModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
