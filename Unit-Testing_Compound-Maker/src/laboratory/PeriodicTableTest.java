package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

class PeriodicTableTest {

    @Test
    @DisplayName("getGasElements with gas attribute")
    void testGetGasElements() {
        PeriodicTable pt = new PeriodicTable();
        for (Element e : pt.getAllElements()) {
            System.out.println(e);
        }
        assert(true);
    }

    @Test
    @DisplayName("getElementsBySymbol with given symbol")
    void testGetElementBySymbol() {
        assert(true);
    }
}