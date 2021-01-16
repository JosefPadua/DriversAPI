# Drivers Api
Jedná se o API aplikaci, kterou jsem zhotovil jako úkol pro zahájení spolupráce.

## Struktura
Na níže uvedeném diagramu je znázorněný vztah mezi classama.

![Diagram](https://ctrlv.cz/shots/2021/01/15/5zci.png)

## Struktura výstupních JSON dat
Výchozí odkaz: 
```
https://localhost:44361/driversdata
```

```
{
    "data": {
        "averageAge": , //Průměrný věk řidičů
        "numberOfBirths": {}, //Roky narození s uvedeným počtem v daném roce
        "sameLastnames": { //Stejná příjmení s uvedenými ID řidiče
            "Romero": [ //Example datas
                12,
                24,
                27
            ],
            "Garcia": [
                36,
                60
            ]
        },
        "mostPopularVehicleModels": {}, //Nejčastější automobilové značky s počtem v garáži
        "averageAgeVehicles": {}, //Průměrné stáří dle značek automobilů
        "engineTypesPrecent": {}, //Uvádí pohon + kolik řidičů v % má tento pohon
        "driversFilter1": [], //Filtr 1: ID všech modrookých řidičů, kteří mají alespoň jedno auto s hybridním nebo elektrickým motorem
        "driversFilter2": [] //Filtr 2: ID řidičů, kteří mají více než jedno auto a všechna auta mají stejný typ motoru
    }
}
```
Špatně uvedený model je nahrazen název "badID"
