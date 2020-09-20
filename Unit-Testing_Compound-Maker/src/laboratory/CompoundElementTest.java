package laboratory;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class CompoundElementTest {
  private CompoundElement compound_Water_1;
  private CompoundElement compound_Water_2;
  private CompoundElement compound_Ammonia;

  @BeforeEach
  void init(){
    // ArrayLists to hold the elements for each compound
    ArrayList<Element> elements_1 = new ArrayList<>();
    ArrayList<Element> elements_2 = new ArrayList<>();
    ArrayList<Element> elements_3 = new ArrayList<>();

    // Create and add Hydrogen and Oxygen to elements_1 (Water)
    Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
    elements_1.add(hydrogen_E1);
    elements_1.add(oxygen_E1);

    // Create and add Hydrogen and Oxygen to elements_2 (Water)
    Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
    elements_2.add(hydrogen_E2);
    elements_2.add(oxygen_E2);

    // Create and add Hydrogen and Nitrogen to elements_3 (Ammonia)
    Element hydrogen_E3 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
    Element nitrogen_E3 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
    elements_3.add(hydrogen_E3);
    elements_3.add(nitrogen_E3);

    // Create compound_1 (Water) and compound_2 (Ammonia)
    compound_Water_1 = new CompoundElement("Water", elements_1);
    compound_Water_2 = new CompoundElement("Water", elements_2);
    compound_Ammonia = new CompoundElement("Ammonia", elements_3);
  }

  @Test
  @DisplayName("Test CompoundNameComparator compare() - Greater Than")
  void testCompare_CompoundNameGreaterThan() {
    // Create the comparator object to compare compound_1 and compound_2 by name
    CompoundNameComparator cnc = new CompoundNameComparator();

    // If the name of compound_1 is greater than the name of compound_2 then the
    // comparator will return a value greater than 0.

    // Assert that the name Water is greater than the name Ammonia
    assertTrue(cnc.compare(compound_Water_1, compound_Ammonia) > 0);
    }

    @Test
    @DisplayName("Test CompoundNameComparator compare() - Less Than")
    void testCompare_CompoundNameLessThan() {
        // Create the comparator object to compare compound_1 and compound_2 by name
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is less than the name of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the name Ammonia is less than the name Water
        assertTrue(cnc.compare(compound_Ammonia, compound_Water_1) < 0);
    }

    @Test
    @DisplayName("Test CompoundNameComparator compare() - Equal To")
    void testCompare_CompoundNameEqualTo() {
        // Create the comparator object to compare compound_1 and compound_2 by name
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is equal to the name of compound_2 then the
        // comparator will return 0.

        // Assert that the name Water is equal to the name Water
        assertEquals(cnc.compare(compound_Water_1, compound_Water_2), 0);
    }

    @Test
    @DisplayName("Test FormulaComparator compare() - Greater Than")
    void testCompare_FormulaGreaterThan() {
        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is greater than the formula name of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the formula for Ammonia is greater than the formula name for Water
        // compound_1 : Formula (Ammonia) : NH3
        // compound_2 : Formula (Water)   : H2O
        assertTrue(fc.compare(compound_Ammonia, compound_Water_1) > 0);
    }

    @Test
    @DisplayName("Test FormulaComparator compare() - Less Than")
    void testCompare_FormulaLessThan() {
        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is less than the formula name of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the formula name for Water is less than the formula name for Ammonia
        // compound_1 : Formula (Water)   : H2O
        // compound_2 : Formula (Ammonia) : NH3
        assertTrue(fc.compare(compound_Water_1, compound_Ammonia) < 0);
    }

    @Test
    @DisplayName("Test FormulaComparator compare() - Equal To")
    void testCompare_FormulaEqualTo() {
        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is equal to the formula name of compound_2 then the
        // comparator will return a value equal to 0.

        // Assert that the formula name for Water is equal to the formula name for Water
        // compound_1 : Formula (Water) : H2O
        // compound_2 : Formula (Water) : H2O
        assertEquals(fc.compare(compound_Water_1, compound_Water_2), 0);
    }

    @Test
    @DisplayName("Test MolecularMassComparator compare() - Greater Than")
    void testCompare_MolecularMassGreaterThan() {
        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is greater than the molecular mass of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the molecular mass of Water is greater than the molecular mass of Ammonia
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Ammonia) : 17.0304
        assertTrue(mmc.compare(compound_Water_1, compound_Ammonia) > 0);
    }

    @Test
    @DisplayName("Test MolecularMassComparator compare() - Less Than")
    void testCompare_MolecularMassLessThan() {
        // Create the comparator object to compare compound_1 and compound_2 by molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is less than the molecular mass of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the molecular mass of Ammonia is less than the molecular mass of Water
        // compound_1 : Molecular Mass (Ammonia) : 17.0304
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertTrue(mmc.compare(compound_Ammonia, compound_Water_1) < 0);
    }

    @Test
    @DisplayName("Test MolecularMassComparator compare() - Equal To")
    void testCompare_MolecularMassEqualTo() {
        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is equal to the molecular mass of compound_2 then the
        // comparator will return a value equal to 0.

        // Assert that the molecular mass of Water is equal to the molecular mass of Water
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertEquals(mmc.compare(compound_Water_1, compound_Water_2), 0);
    }
  
  @Test
  @DisplayName("Test computeFormula() - Ordered Elements")
  void testComputeFormula_OrderedElements() {
    // Create actual formula name
    String water_Formula = "H2O";

    // Assert that the compound of 2 Hydrogen and 1 Oxygen elements equals the water_Formula: H2O
    assertEquals(water_Formula, compound_Water_1.getFormula());
  }

  @Test
  @DisplayName("Test computeFormula() - Unordered Elements")
  void testComputeFormula_UnorderedElements() {
    // ArrayList to hold the elements for the Water compound
    ArrayList<Element> elements = new ArrayList<>();

    // Create and add Oxygen and Hydrogen to elements (Water)
    Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
    Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    elements.add(oxygen);
    elements.add(hydrogen);

    // Create Water compound
    CompoundElement compound = new CompoundElement("Water", elements);

    // Create actual formula name
    String water_Formula = "H2O";

    // Assert that the compound of 1 Oxygen and 2 Hydrogen elements equals the water_Formula: H2O
    assertEquals(water_Formula, compound.getFormula());
  }

  @Test
  @DisplayName("Test computeMolecularMass()")
  void testComputeMolecularMass() {
    // Actual Molecular Mass of Water and Ammonia
    double water_MolMass = 18.0152;
    double ammonia_MolMass = 17.0304;

    // Assert that the actual Molecular Mass of Water and Ammonia equals the value returned from getMolecularMass()
    assertAll(() -> assertEquals(water_MolMass, compound_Water_1.getMolecularMass()),
        () -> assertEquals(ammonia_MolMass, compound_Ammonia.getMolecularMass()));
  }

  @Test
  @DisplayName("Test computeHillFormula() - Fail Expected")
  void test_computeHillFormula_SulfuricAcid(){
    // Create Elements Hydrogen, Sulfur and Oxygen to make Sulfuric acid.
    Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
    Element sulfur = new Element(16, "Sulfur", "S", 32.065, 1, 8, 1);
    Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 4);

    // Create an arraylist to store the elements, and to make a compound from.
    ArrayList<Element> sulfuricAcidElements = new ArrayList<>();
    sulfuricAcidElements.add(hydrogen);
    sulfuricAcidElements.add(sulfur);
    sulfuricAcidElements.add(oxygen);

    // Create an instance of a compound.
    CompoundElement sulfuricAcid = new CompoundElement("Sulfuric Acid", sulfuricAcidElements);

    String sulfuricAcidHillFormula = "H204S";


    // We are asserting this these values are not equals rather than having the test fail because it is not able to correctly compute
    // the hill formula.

    assertEquals(sulfuricAcidHillFormula, sulfuricAcid.getHillFormula());
  }
  @Test
  @DisplayName("Test computeHillFormula() - Pass Expected")
  void test_computeHillFormula(){
    String ammoniaHillFormula = "H3N";
    String waterHillFormula = "H2O";

    assertEquals(waterHillFormula, compound_Water_1.getHillFormula());
    assertEquals(ammoniaHillFormula, compound_Ammonia.getHillFormula());
  }

  @Test
  @DisplayName("Test HillFormulaComparator compare() - Greater Than")
  void test_HillCompareGreater(){
    HillFormulaComparator hFC = new HillFormulaComparator();

    assertTrue(hFC.compare(compound_Ammonia, compound_Water_1) > 0);
  }

  @Test
  @DisplayName("Test HillFormulaComparator compare() - Less Than")
  void test_HillCompareLess(){
    HillFormulaComparator hFC = new HillFormulaComparator();

    assertTrue(hFC.compare(compound_Water_1, compound_Ammonia) < 0);
  }

  @Test
  @DisplayName("Test HillFormulaComparator compare() - Equal To")
  void test_HillCompareEqual(){
    HillFormulaComparator hFC = new HillFormulaComparator();

    assertTrue(hFC.compare(compound_Water_1, compound_Water_2) == 0);
  }


  @Test
  @DisplayName("Test compareTo() - Greater Than")
  void test_compareToGreaterThan(){
    assertTrue(compound_Water_1.compareTo(compound_Ammonia) > 0);
  }

  @Test
  @DisplayName("Test compareTo() - Less Than")
  void test_compareToLessThan(){
    assertTrue(compound_Ammonia.compareTo(compound_Water_1) < 0);
  }

  @Test
  @DisplayName("Test compareTo() - Equal To")
  void test_compareToEquals(){
    assertEquals(compound_Water_1.compareTo(compound_Water_2), 0);
  }

  @Test
  @DisplayName("Test equals()")
  void test_equals(){
    // Using the two created instances of water, prove that they are equal.
    assertTrue(compound_Water_1.equals(compound_Water_2));
  }

}