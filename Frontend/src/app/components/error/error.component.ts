import { OnInit, Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html'
})

export class ErrorComponent implements OnInit {

  public message: string = "There was an error with our server please try again.";

  constructor(){

  }
    
  ngOnInit() {
  }

  public onReloadPage(): void {
    location.reload();
  }
}
