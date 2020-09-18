package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class FormulaComparatorTest {

    @Test
    @DisplayName("Test Formula Greater Than")
    void testCompare_GreaterThan() {
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
    void testCompare_LessThan() {
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
    void testComapare_EqualTo() {
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
}