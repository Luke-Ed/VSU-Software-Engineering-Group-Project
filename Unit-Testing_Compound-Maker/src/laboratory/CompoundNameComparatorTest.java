package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class CompoundNameComparatorTest {

    @Test
    @DisplayName("Test Compound Name Greater Than")
    void testCompare_CompoundNameGreaterThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<Element>();
        ArrayList<Element> elements_2 = new ArrayList<Element>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (NH3)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(nitrogen_E2);

        // Create compound_1 (Water) and compound_2 (NH3)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("NH3", elements_2);

        // Create the comparator object to compare compound_1 and compound_2
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is greater than the name of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the name Water is greater than the name NH3
        assertTrue(cnc.compare(compound_1, compound_2) > 0);
    }

    @Test
    @DisplayName("Test Compound Name Less Than")
    void testCompare_CompoundNameLessThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<Element>();
        ArrayList<Element> elements_2 = new ArrayList<Element>();

        // Create and add Hydrogen and Nitrogen to elements_1 (NH3)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E1 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(nitrogen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (NH3) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("NH3", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2
        CompoundNameComparator cnc = new CompoundNameComparator();

        // If the name of compound_1 is less than the name of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the name NH3 is less than the name Water
        assertTrue(cnc.compare(compound_1, compound_2) < 0);
    }

    @Test
    @DisplayName("Test Compound Name Equal To")
    void testCompare_CompoundNameEqualTo() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<Element>();
        ArrayList<Element> elements_2 = new ArrayList<Element>();

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
        assertTrue(cnc.compare(compound_1, compound_2) == 0);
    }
}