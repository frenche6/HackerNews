import { Component, Inject, AfterViewInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { StoryItem } from "../../Interfaces/story-item"

@Component({
  selector: 'app-news-search',
  templateUrl: './news-search.component.html',
  styleUrls: ['./news-search.component.css']
})
export class NewsSearchComponent implements AfterViewInit {

  stories: StoryItem[];
  filteredStories: StoryItem[];
  paginationConfig: any;
  searchText: string;

  httpClient: HttpClient;
  baseUrl: string;

  itemsPerPage: number = 10;
  isPageLoading: boolean = false;
  totalNumberOfRecordsToGet: number = 150;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;    
  }

  ngAfterViewInit() {
       this.getNewsStories();
  }

  getNewsStories(){
    this.isPageLoading = true;
    this.httpClient.get<StoryItem[]>(this.baseUrl + 'NewsStories/GetNewStories/' + this.totalNumberOfRecordsToGet).subscribe(result => {
      this.stories = result;
      this.filteredStories = result;
      this.setPaginationConfig(this.itemsPerPage, 0, result.length);      
      this.isPageLoading = false;
    }, error => {
      this.isPageLoading = false;
      console.error(error);
    });
  }
 

  pageChanged(event){
    if (this.isPageLoading)
      return;

    this.paginationConfig.currentPage = event;
  }

  setPaginationConfig(itemsPerPage: number, currentPage: number, totalItems: number){
    this.paginationConfig = {
      itemsPerPage: itemsPerPage,
      currentPage: currentPage,
      totalItems: totalItems
    };
  }

  searchBoxKeyUp(event: any){

    this.filteredStories = this.stories.filter(story => story != null && story.title != null && story.title.toLocaleLowerCase().includes(event.target.value.toLocaleLowerCase()));
    this.setPaginationConfig(this.itemsPerPage, 1, this.filteredStories.length);
  }

}
