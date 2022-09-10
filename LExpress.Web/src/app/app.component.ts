import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './shared/models/Pagination';
import { IProduct } from './shared/models/Product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'L-Express!';
  products: IProduct[];

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.httpClient.get('https://localhost:5001/api/products?pageSize=6')
      .subscribe((response: IPagination) => {
        this.products = response.data;
      });
  }
}
