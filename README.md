# ElectricalAppliancesApp
Console app for managing and testing electrical appliances

## Features

- Reads electrical appliances data from a file
- Supports different appliance types:
  - VacuumCleaner
  - WashingMachine
  - FoodProcessor
- Filters appliances by brand
- Outputs:
  - Sorted appliance counts
  - Brand-specific data with total price
- JSON serialization/deserialization
- Logging via log4net
- Unit tests using MSTest

## How to run

1. Place input file `Electrical appliance.txt` on your Desktop.
2. Run the program.
3. Follow console instructions.

## Output

- `file1.txt` — sorted appliance names with counts
- `file2.txt` — brand-specific report
- `appliances.json` — serialized appliance data
