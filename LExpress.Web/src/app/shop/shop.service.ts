import { HttpParams } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IPagination } from '../shared/models/Pagination';
import { IProductBrand } from '../shared/models/ProductBrand';
import { IProductType } from '../shared/models/ProductType';
import { ShopParams } from '../shared/models/ShopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = "https://localhost:5001/api/";

  constructor(private httpClient: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId.toString());
    if (shopParams.typeId > 0) params = params.append('typeId', shopParams.typeId.toString());
    if (shopParams.search) params = params.append('productName', shopParams.search);

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.httpClient.get<IPagination>(this.baseUrl + "products", { observe: 'response', params })
      .pipe(map(response => {
        return response.body;
      }));
  }

  getBrands() {
    return this.httpClient.get<IProductBrand[]>(this.baseUrl + "products/brands");
  }

  getTypes() {
    return this.httpClient.get<IProductType[]>(this.baseUrl + "products/types");
  }
}
