import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { startLoading, stopLoading } from '../store/loading/loading.actions';
import { selectIsLoading } from '../store/loading/loading.selectors';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  constructor(private store: Store) {}

  isLoading$: Observable<boolean> = this.store.select(selectIsLoading);

  show(): void {
    this.store.dispatch(startLoading());
  }

  hide(): void {
    this.store.dispatch(stopLoading());
  }
}