package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ElementTest{
     
     @Test
     @DisplayName("Testing for positive difference in atomic numbers.")
     void testCompareTo_positive_difference(){
          //Creating Elements to test.
          Element Fluorine = new Element(9, "Fluorine", "F", 18.9984, 3, 8);
          Element Sulfur = new Element(16, "Sulfur", "S", 32.065, 1, 8);
	  
	  //Assert that the atomic number of Sulfur (16) is larger than that of Fluorine (9).
          assertTrue(Sulfur.compareTo(Fluorine) > 0);
     }

     @Test
     @DisplayName("Testing for zero difference in atomic numbers.")
     void testCompareTo_zero_difference(){
	  //Creating Elements to test.
          Element Molybdenum1 = new Element(42, "Molybdenum", "Mo", 95.94, 1, 5);
          Element Molybdenum2 = new Element(42, "Molybdenum", "Mo", 95.94, 1, 5);
	  
	  //Assert that the atomic number of two Molybdenum elements are equal.
          assertTrue(Molybdenum1.compareTo(Molybdenum2) == 0);
     }

     @Test
     @DisplayName("Testing for negative difference in atomic numbers.")
     void testCompareTo_negative_difference(){
	  //Creating Elements to test.
          Element Bromine = new Element(35, "Bromine", "Br", 79.904, 2, 8);
          Element Platinum = new Element(78, "Platinum", "Pt", 192.217, 1, 5);
	  
	  //Assert that the atomic number of Bromine (35) is smaller than that of Platinum (78).
          assertTrue(Bromine.compareTo(Platinum) < 0);
     }
}