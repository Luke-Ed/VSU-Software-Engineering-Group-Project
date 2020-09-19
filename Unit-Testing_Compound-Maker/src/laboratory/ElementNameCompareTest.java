package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ElementNameCompareTest{
	  
     @Test
     @DisplayName("Testing for a positive difference between element names.")
     void testCompare_larger_element_name(){
          //Creating Elements to test.
	  Element Osmium = new Element(76, "Osmium", "Os", 190.23, 1, 5);
	  Element Magnesium = new Element(12, "Magnesium", "Mg", 24.305, 1, 2);

	  //Create the Element Name comparator.
	  ElementNameCompare comp = new ElementNameCompare();

	  //Assert that the name "Osmium" comes after "Magnesium".
	  assertTrue(comp.compare(Osmium, Magnesium) > 0);
     }

     @Test
     @DisplayName("Testing for no difference between element names.")
     void testCompare_same_element_name(){
          //Creating Elements to test.
	  Element Calcium1 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);
	  Element Calcium2 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);

	  //Create the Element Name comparator.
	  ElementNameCompare comp = new ElementNameCompare();

          //Assert that the names are the same.
	  assertTrue(comp.compare(Calcium1, Calcium2) == 0);
     }

     @Test
     @DisplayName("Testing for a negative difference between element names.")
     void testCompare_smaller_element_name(){
          //Creating Elements to test.
	  Element Carbon = new Element(6, "Carbon", "C", 12.0107, 1, 8);
	  Element Nitrogen = new Element(7, "Nitrogen", "N", 14.0067, 3, 8);

	  //Create the Element Name comparator.
	  ElementNameCompare comp = new ElementNameCompare();

          //Assert that the name "Carbon" comes before "Nitrogen".
	  assertTrue(comp.compare(Carbon, Nitrogen) < 0);
     }
} 