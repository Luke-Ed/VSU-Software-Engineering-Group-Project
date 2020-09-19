package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class SymbolCompareTest{
	  
     @Test
     @DisplayName("Testing for a positive difference between element symbols.")
     void testCompare_larger_element_symbol(){
          //Creating Elements to test. (Elements with chemical symbols that differ from their element name were chosen.)
	  Element Potassium = new Element(19, "Potassium", "K", 39.0983, 1, 1);
	  Element Lead = new Element(82, "Lead", "Pb", 207.2, 1, 6);

	  //Create the Element Symbol comparator.
	  SymbolCompare comp = new SymbolCompare();

	  //Assert that the symbol "Pb" comes after "K".
	  assertTrue(comp.compare(Lead, Potassium) > 0);
     }

     @Test
     @DisplayName("Testing for no difference between element symbols.")
     void testCompare_same_element_symbol(){
          //Creating Elements to test.
	  Element Calcium1 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);
	  Element Calcium2 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);

	  //Create the Element Symbol comparator.
	  SymbolCompare comp = new SymbolCompare();

          //Assert that the symbols are the same.
	  assertTrue(comp.compare(Calcium1, Calcium2) == 0);
     }

     @Test
     @DisplayName("Testing for a negative difference between element symbols.")
     void testCompare_smaller_element_symbol(){
          //Creating Elements to test. (Elements with chemical symbols that differ from their element name were chosen.)
	  Element Potassium = new Element(19, "Potassium", "K", 39.0983, 1, 1);
	  Element Lead = new Element(82, "Lead", "Pb", 207.2, 1, 6);

	  //Create the Element Symbol comparator.
	  SymbolCompare comp = new SymbolCompare();

	  //Assert that the symbol "K" comes before "Pb".
	  assertTrue(comp.compare(Potassium, Lead) < 0);
     }
} 