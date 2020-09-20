package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ElementTest{

  @Test
  @DisplayName("Test compareTo - Greater Than")
  void testCompareTo_positive_difference(){
    //Creating Elements to test.
    Element Fluorine = new Element(9, "Fluorine", "F", 18.9984, 3, 8);
    Element Sulfur = new Element(16, "Sulfur", "S", 32.065, 1, 8);

    //Assert that the atomic number of Sulfur (16) is larger than that of Fluorine (9).
    assertTrue(Sulfur.compareTo(Fluorine) > 0);
  }

  @Test
  @DisplayName("Test compareTo - Equal To")
  void testCompareTo_zero_difference(){
    //Creating Elements to test.
    Element Molybdenum1 = new Element(42, "Molybdenum", "Mo", 95.94, 1, 5);
    Element Molybdenum2 = new Element(42, "Molybdenum", "Mo", 95.94, 1, 5);

    //Assert that the atomic number of two Molybdenum elements are equal.
    assertTrue(Molybdenum1.compareTo(Molybdenum2) == 0);
  }

  @Test
  @DisplayName("Test compareTo - Less Than")
  void testCompareTo_negative_difference(){
    //Creating Elements to test.
    Element Bromine = new Element(35, "Bromine", "Br", 79.904, 2, 8);
    Element Platinum = new Element(78, "Platinum", "Pt", 192.217, 1, 5);

    //Assert that the atomic number of Bromine (35) is smaller than that of Platinum (78).
    assertTrue(Bromine.compareTo(Platinum) < 0);
  }

  @Test
  @DisplayName("Test ElementNameCompare compare - Greater Than")
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
  @DisplayName("Test ElementNameCompare compare - Equal To")
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
  @DisplayName("Test ElementNameCompare compare - Less Than")
  void testCompare_smaller_element_name(){
    //Creating Elements to test.
    Element Carbon = new Element(6, "Carbon", "C", 12.0107, 1, 8);
    Element Nitrogen = new Element(7, "Nitrogen", "N", 14.0067, 3, 8);

    //Create the Element Name comparator.
    ElementNameCompare comp = new ElementNameCompare();

    //Assert that the name "Carbon" comes before "Nitrogen".
    assertTrue(comp.compare(Carbon, Nitrogen) < 0);
  }

  @Test
  @DisplayName("Test AtomicNumberComparator compare - Greater Than")
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
  @DisplayName("Test AtomicNumberComparator compare - Equal To")
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
  @DisplayName("Test AtomicNumberComparator compare - Less Than")
  void testCompare_negative_difference(){
    //Creating Elements to test.
    Element Carbon = new Element(6, "Carbon", "C", 12.0107, 1, 8);
    Element Nitrogen = new Element(7, "Nitrogen", "N", 14.0067, 3, 8);

    //Create the Atomic Number comparator.
    AtomicNumberComparator comp = new AtomicNumberComparator();

    //Assert that the difference between the atomic numbers of Nitrogen and Carbon is negative.
    assertTrue(comp.compare(Carbon, Nitrogen) < 0);
  }

  @Test
  @DisplayName("Test MassCompare compare - Greater Than")
  void testCompare_larger_atomic_mass(){
    //Creating Elements to test.
    Element Osmium = new Element(76, "Osmium", "Os", 190.23, 1, 5);
    Element Magnesium = new Element(12, "Magnesium", "Mg", 24.305, 1, 2);

    //Create the Atomic Mass comparator.
    MassCompare comp = new MassCompare();

    //Assert that the difference between the atomic masses of Osmium and Magnesium is positive.
    assertTrue(comp.compare(Osmium, Magnesium) > 0);
  }

  @Test
  @DisplayName("Test MassCompare compare - Equal To")
  void testCompare_same_atomic_mass(){
    //Creating Elements to test.
    Element Calcium1 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);
    Element Calcium2 = new Element(20, "Calcium", "Ca", 40.078, 1, 2);

    //Create the Atomic Mass comparator.
    MassCompare comp = new MassCompare();

    //Assert that the difference between the atomic masses of the Calcium elements is zero.
    assertTrue(comp.compare(Calcium1, Calcium2) == 0);
  }

  @Test
  @DisplayName("Test MassCompare compare - Less Than")
  void testCompare_smaller_atomic_mass(){
    //Creating Elements to test.
    Element Carbon = new Element(6, "Carbon", "C", 12.0107, 1, 8);
    Element Nitrogen = new Element(7, "Nitrogen", "N", 14.0067, 3, 8);

    //Create the Atomic Mass comparator.
    MassCompare comp = new MassCompare();

    //Assert that the difference between the atomic masses of Nitrogen and Carbon is negative.
    assertTrue(comp.compare(Carbon, Nitrogen) < 0);
  }

  @Test
  @DisplayName("Test SymbolCompare compare - Greater Than - Fail Expected")
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
  @DisplayName("Test SymbolCompare compare - Equal To - Fail Expected")
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
  @DisplayName("Test SymbolCompare compare - Less Than")
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