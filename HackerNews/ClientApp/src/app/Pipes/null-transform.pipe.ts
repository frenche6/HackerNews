import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "nullTransform",
})
export class NullTransformPipe implements PipeTransform {
  transform(value: any, returnText: string): any {
    if (value != null) return value;
    else return returnText ? returnText : "";
  }
}
