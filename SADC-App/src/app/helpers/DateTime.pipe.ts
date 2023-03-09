import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DateTime'
})
export class DateTimePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return null;
  }

}
