# Tentamen i Clean code och testbar kod

## Vad du valt att testa och varför?

Jag har valt att testa de GET metoderna i `MovieController` för att säkerställa att de returnerar rätt resultat.
- `TopList()` - jag har testat om metoden returnerar rätt sorterade data baserat på `ascending` parameters value.
- `GetMovieById` - jag har testat om den returnerar rätt status code (200, 404), baserat på om id funnits eller inte.
- `GetUniqueMovies` - jag har testat om den returnerar unika värden utan dubletter.
För att testerna ska fungera i isolation har jag skapat en `IApiCaller` interface och en mockobjekt som ersätter anrop till http-klienten.

## Vilket/vilka designmönster har du valt, varför? Hade det gått att göra på ett annat sätt?

Jag har valt att implementera Factory Pattern för att hantera HTTP responses utan att exponera vilken specifik subklass av `Response` som ska skapas. Samtidigt lyckades jag att implementera även två antipatterns: overingineering & non-invented-here-syndrome. :) 

Nu inser jag (too late) att jag har missat att anpassa data från två olika endpoints som jag konkatenerar i `GetUniqueMovies`. Model property `rated` har ett annat namn i den andra endpointen, vilket gör att den tappas bort när man fetchar data. Detta har jag kunnat lösa med en Adapter pattern som skulle göra dem kompatibla med varandra.

## Hur mycket valde du att optimera koden, varför är det en rimlig nivå för vårt program?

Jag har valt att förbättra namning för att variablerna hade otydliga namn samt att förenkla en del komplicerade uttryck (t ex `if`-satser eller `for`-loops) med lambda / linq funktioner/queries. Dessutom har jag använt Dependency Injection för att "injecta" `HttpClient` samt egna service-klasser i kontrollern.
