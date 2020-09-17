import { Component, Inject, AfterViewInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { StoryItem } from "../Interfaces/story-item";

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements AfterViewInit {
  totalStoryCount: number;
  stories: StoryItem[];
  filteredStories: StoryItem[];
  paginationConfig: any;
  searchText: string;

  httpClient: HttpClient;
  baseUrl: string;

  itemsPerPage: number = 12;
  isPageLoading: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;    
  }

  ngAfterViewInit() {
       this.getNewsStories();
  }

  getNewsStories(){
    this.isPageLoading = true;
    this.httpClient.get<number>(this.baseUrl + 'NewsStories/GetNewStoriesCount').subscribe(result => {
      this.totalStoryCount = result;
      this.setPaginationConfig(this.itemsPerPage, 0, result);
      this.getNewsStoriesPaginated();
      this.isPageLoading = false;
    }, error => {
      this.isPageLoading = false;
      console.error(error);
    });
  }

  getNewsStoriesPaginated(){
    this.isPageLoading = true;
    this.httpClient.get<StoryItem[]>(this.baseUrl + 'NewsStories/GetNewStories/' + this.paginationConfig.currentPage + '/' + this.paginationConfig.itemsPerPage).subscribe(result => {
      this.stories = result;
      this.filteredStories = result;
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
    this.getNewsStoriesPaginated();
  }

  setPaginationConfig(itemsPerPage: number, currentPage: number, totalItems: number){
    this.paginationConfig = {
      itemsPerPage: itemsPerPage,
      currentPage: currentPage,
      totalItems: totalItems
    };
  }

  onKey(event: any){
    this.filteredStories = this.stories.filter(story => story != null && story.title != null && story.title.includes(event.target.value));
    this.setPaginationConfig(this.itemsPerPage, 1, this.filteredStories.length);
  }

}
