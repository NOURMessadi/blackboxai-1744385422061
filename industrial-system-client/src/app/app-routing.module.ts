import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SampleFormComponent } from './components/sample-form/sample-form.component';

const routes: Routes = [
  { path: 'sample-form', component: SampleFormComponent },
  { path: '', redirectTo: '/sample-form', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
