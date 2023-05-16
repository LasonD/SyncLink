import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from "./app-routing/app-routing.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { StoreModule } from "@ngrx/store";
import { appReducer } from "./store/app.reducer";
import { AuthEffects } from "./auth/store/auth.effects";
import { EffectsModule } from "@ngrx/effects";
import { HttpClientModule } from "@angular/common/http";
import { CoreModule } from "./core/core.module";
import { ToastrModule } from "ngx-toastr";
import { GroupSearchEffects } from "./groups/groups-search/store/groups-search.effects";
import { CreateGroupEffects } from "./groups/create-group/store/create-group.effects";
import { HeaderComponent } from './header/header.component';
import { GroupHubEffects } from "./groups/group-hub/store/group-hub.effects";
import { RoomEffects } from "./rooms/store/rooms.effects";
import { NotificationEffects } from "./common/store/notifications.effects";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    MatSlideToggleModule,
    StoreModule.forRoot(appReducer),
    EffectsModule.forRoot([AuthEffects, GroupSearchEffects, CreateGroupEffects, GroupHubEffects, RoomEffects, NotificationEffects]),
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
