package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class CompoundElementTest {

    @Test
    @DisplayName("Test compute formula with ordered elements")
    void testComputeFormula_OrderedElements() {
        // ArrayList to hold the elements for the Water compound
        ArrayList<Element> elements = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements (Water)
        Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements.add(hydrogen);
        elements.add(oxygen);

        // Create Water compound
        CompoundElement compound = new CompoundElement("Water", elements);

        // Create actual formula name
        String water_Formula = "H2O";

        // Assert that the compound of 2 Hydrogen and 1 Oxygen elements equals the water_Formula: H2O
        assertEquals(water_Formula, compound.getFormula());
    }

    @Test
    @DisplayName("Test compute formula with unordered elements")
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
        // Note: This test fails due to application program not correctly computing the proper formula
        assertEquals(water_Formula, compound.getFormula());
    }

    @Test
    @DisplayName("Test compute molecular mass")
    void testComputeMolecularMass() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(nitrogen_E2);

        // Create compound_1 (Water) and compound_2 (Ammonia)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);

        // Actual Molecular Mass of Water and Ammonia
        double water_MolMass = 18.0152;
        double ammonia_MolMass = 17.0304;

        // Assert that the actual Molecular Mass of Water and Ammonia equals the value returned from getMolecularMass()
        assertAll(() -> assertEquals(water_MolMass, compound_1.getMolecularMass()),
                  () -> assertEquals(ammonia_MolMass, compound_2.getMolecularMass()));
    }

    @Test
    @DisplayName("Test Compound Name Greater Than")
    void testCompare_CompoundNameGreaterThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(nitrogen_E2);

        // Create compound_1 (Water) and compound_2 (Ammonia)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by name
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is greater than the name of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the name Water is greater than the name Ammonia
        assertTrue(cnc.compare(compound_1, compound_2) > 0);
    }

    @Test
    @DisplayName("Test Compound Name Less Than")
    void testCompare_CompoundNameLessThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Nitrogen to elements_1 (Ammonia)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E1 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(nitrogen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (Ammonia) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Ammonia", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by name
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is less than the name of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the name Ammonia is less than the name Water
        assertTrue(cnc.compare(compound_1, compound_2) < 0);
    }

    @Test
    @DisplayName("Test Compound Name Equal To")
    void testCompare_CompoundNameEqualTo() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

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

        // Create compound_1 (Water) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by name
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is equal to the name of compound_2 then the
        // comparator will return 0.

        // Assert that the name Water is equal to the name Water
        assertEquals(cnc.compare(compound_1, compound_2), 0);
    }

    @Test
    @DisplayName("Test Formula Greater Than")
    void testCompare_FormulaGreaterThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Nitrogen to elements_1 (Ammonia)
        Element nitrogen_E1 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        elements_1.add(nitrogen_E1);
        elements_1.add(hydrogen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (Ammonia) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Ammonia", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is greater than the formula name of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the formula for Ammonia is greater than the formula name for Water
        // compound_1 : Formula (Ammonia) : NH3
        // compound_2 : Formula (Water)   : H2O
        assertTrue(fc.compare(compound_1, compound_2) > 0);
    }

    @Test
    @DisplayName("Test Formula Less Than")
    void testCompare_FormulaLessThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        elements_2.add(nitrogen_E2);
        elements_2.add(hydrogen_E2);

        // Create compound_1 (Water) and compound_2 (Ammonia)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is less than the formula name of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the formula name for Water is less than the formula name for Ammonia
        // compound_1 : Formula (Water)   : H2O
        // compound_2 : Formula (Ammonia) : NH3
        assertTrue(fc.compare(compound_1, compound_2) < 0);
    }

    @Test
    @DisplayName("Test Formula Equal To")
    void testCompare_FormulaEqualTo() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

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

        // Create compound_1 (Water) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by formula
        FormulaComparator fc = new FormulaComparator();

        // If the formula name of compound_1 is equal to the formula name of compound_2 then the
        // comparator will return a value equal to 0.

        // Assert that the formula name for Water is equal to the formula name for Water
        // compound_1 : Formula (Water) : H2O
        // compound_2 : Formula (Water) : H2O
        assertEquals(fc.compare(compound_1, compound_2), 0);
    }

    @Test
    @DisplayName("Test Molecular Mass Greater than")
    void testCompare_MolecularMassGreaterThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(nitrogen_E2);

        // Create compound_1 (Water) and compound_2 (Ammonia)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is greater than the molecular mass of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the molecular mass of Water is greater than the molecular mass of Ammonia
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Ammonia) : 17.0304
        assertTrue(mmc.compare(compound_1, compound_2) > 0);
    }

    @Test
    @DisplayName("Test Molecular Mass Less than")
    void testCompare_MolecularMassLessThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Nitrogen to elements_1 (Ammonia)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E1 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(nitrogen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (Ammonia) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Ammonia", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is less than the molecular mass of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the molecular mass of Ammonia is less than the molecular mass of Water
        // compound_1 : Molecular Mass (Ammonia) : 17.0304
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertTrue(mmc.compare(compound_1, compound_2) < 0);
    }

    @Test
    @DisplayName("Test Molecular Mass Equal To")
    void testCompare_MolecularMassEqualTo() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

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

        // Create compound_1 (Water) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is equal to the molecular mass of compound_2 then the
        // comparator will return a value equal to 0.

        // Assert that the molecular mass of Water is equal to the molecular mass of Water
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertEquals(mmc.compare(compound_1, compound_2), 0);
    }
}