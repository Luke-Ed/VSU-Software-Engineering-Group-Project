package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class AtomicNumberComparatorTest{
	  
     @Test
     @DisplayName("Testing for a positive difference in atomic numbers.")
     void testCompare_positive_difference(){
          //Creating Elements to test.
	  Element Osmium = new Element(76, "Osmium", "Os", 190.23, 1, 5);
	  Element Magnesium = new Element(12, "Magnesium", "Mg", 24.305, 1, 2);

	  //Create the Atomic Number comparator.
	  AtomicNumberComparator comp = new AtomicNumberComparator();

	  //Assert that the difference between the atomic numbers of Osmium and Magnesium is positive.
	  assertTrue(comp.compare(Osmium, Magnesium) > 0);
     }

     @Test
     @DisplayName("Testing for no difference in atomic numbers.")
     void testCompare_zero_difference(){
          //Creating Elements to test.
	  Element Calcium1 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);
	  Element Calcium2 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);

	  //Create the Atomic Number comparator.
	  AtomicNumberComparator comp = new AtomicNumberComparator();

          //Assert that the difference between the atomic numbers of the Calcium elements is zero.
	  assertTrue(comp.compare(Calcium1, Calcium2) == 0);
     }

     @Test
     @DisplayName("Testing for a negative difference in atomic numbers.")
     void testCompare_negative_difference(){
          //Creating Elements to test.
	  Element Carbon = new Element(6, "Carbon", "C", 12.0107, 1, 8);
	  Element Nitrogen = new Element(7, "Nitrogen", "N", 14.0067, 3, 8);

	  //Create the Atomic Number comparator.
	  AtomicNumberComparator comp = new AtomicNumberComparator();

          //Assert that the difference between the atomic numbers of Nitrogen and Carbon is negative.
	  assertTrue(comp.compare(Carbon, Nitrogen) < 0);
     }
} 