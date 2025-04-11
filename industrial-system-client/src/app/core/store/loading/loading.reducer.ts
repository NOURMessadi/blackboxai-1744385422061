import { createReducer, on } from '@ngrx/store';
import * as LoadingActions from './loading.actions';

export interface LoadingState {
  isLoading: boolean;
}

export const initialState: LoadingState = {
  isLoading: false
};

export const loadingReducer = createReducer(
  initialState,
  on(LoadingActions.startLoading, state => ({ ...state, isLoading: true })),
  on(LoadingActions.stopLoading, state => ({ ...state, isLoading: false }))
);