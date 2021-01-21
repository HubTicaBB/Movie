# Tentamen i Clean code och testbar kod

## Vad du valt att testa och varför?
Jag har valt at testa om requests returnerar rätt / rätt sorterad data
EDIT (sen): Riktiga tester gick borta... De som jag lämnat in hanterar en gammal version av Controllern...

## Vilket/vilka designmönster har du valt, varför? Hade det gått att göra på ett annat sätt?
Implementerat Factory Method för att hantera http responses utan att "expose" vilken subclass av Response class det är.

## Hur mycket valde du att optimera koden, varför är det en rimlig nivå för vårt program?
Jag har valt att förbättra namning för att variabelna hade otydliga namn samt att förenkla en del komplicerade uttryck (t ex if eller forloop) med lambda / linq funktioner/queriwes
