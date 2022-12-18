import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RatingComponent } from './rating.component';

describe('BookRatingComponent', () => {
  let component: RatingComponent;
  let fixture: ComponentFixture<RatingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RatingComponent]
    });
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RatingComponent);
    component = fixture.componentInstance;
  });

  describe('when the rating has no remainder', () => {
    it('shows five empty stars when the rating is zero', () => {
      component.rating = 0;
      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(5);
    });

    it('shows zero empty stars when the rating is five', () => {
      component.rating = 5;
      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(0);
    });

    it('shows "five minus rating" empty stars', () => {
      component.rating = 3;

      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(2);
    });

    it('shows five full stars when the rating is five', () => {
      component.rating = 5;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(5);
    });

    it('shows zero full stars when the rating is zero', () => {
      component.rating = 0;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(0);
    });

    it('shows "rating" full stars', () => {
      component.rating = 3;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(3);
    });

    it('shows no half star', () => {
      component.rating = 3;
      fixture.detectChanges();

      expect(getHalfStar()).toBeNull();
    });
  });

  describe('when the rating has a remainder', () => {
    const ratingWithRemainder = 2.5;

    it('shows a half star', () => {
      component.rating = ratingWithRemainder;

      fixture.detectChanges();

      expect(getHalfStar()).not.toBeNull();
    });

    it('shows 5 - (ratingWithRemainder + 0.5) empty stars', () => {
      component.rating = ratingWithRemainder;

      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(2);
    });

    it('shows ratingWithRemainder - 0.5 full stars', () => {
      component.rating = ratingWithRemainder;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(2);
    });
  });

  function getEmptyStars(): HTMLSpanElement[] {
    return fixture.nativeElement.querySelectorAll('[data-jest="empty star"]');
  }

  function getFullStars(): HTMLSpanElement[] {
    return fixture.nativeElement.querySelectorAll('[data-jest="full star"]');
  }

  function getHalfStar(): HTMLSpanElement {
    return fixture.nativeElement.querySelector('[data-jest="half star"]');
  }
});
