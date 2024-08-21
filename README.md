**MLAgents Deel III – JumperAgent
**
**Inleiding**

Het doel van dit project is een mlagent succesvol trainen om over obstakels te springen. De applicatie bestaat uit 3 soorten objecten; de agent, de obstakels en de muur achter de agent. Het doel van de agent is ervoor zorgen dat de obstakels de muur aanraken zonder dat ze zichzelf aanraken. Dit is de manier voor hem om punten te scoren.

**Opbouw**

![image](https://github.com/user-attachments/assets/222d351a-be8a-4927-9f7b-f4c63d22881e)
![image](https://github.com/user-attachments/assets/f0c82c79-de7e-434d-93bb-3b93bb75378f)

**.yaml configuratie**

Dit zijn de settings in de yaml file die ik gebruikt heb voor het trainen van de agent:
![image](https://github.com/user-attachments/assets/679cee08-55c9-4c4e-85c2-0ed4e2734357)

 
**Actions**

De agent beschikt over twee verschillende acties: springen en niets doen. Als hij kiest voor springen zal een vectorkracht in de y-as richting bovenwaarts uitgeoefend worden op het agentobject.

**Rewards system**

Voor het rewards systeem heb ik ervoor gekozen enkel punten bij te geven als hij iets goed doet en geen punten af te trekken. Als het obstakel de muur achter de agent aanraakt verdient hij 1.0 punten. Om ervoor te zorgen dat de agent niet constant blijft springen heb ik hem ook punten gegeven wanneer hij stilstaat; 0.1 * Time.deltaTime. Deze puntenverdeling werkte het best tijdens het trainen.

**Trainingsproces**

Zoals eerder vermeld ging het trainingsproces pas heel vlot wanneer ik niet meer met negatieve puntenverdeling werkte.
![image](https://github.com/user-attachments/assets/831d62a1-f4fb-4095-a23b-941c0b4910b1)

De JumperAgent1.0 en 2.0 getuigen van zo’n puntenverdeling. Daarna kreeg ik beter resultaten:
![image](https://github.com/user-attachments/assets/a30083ee-0932-435c-a0be-22da625af770)

Deze versie heb ik dan ook langer laten trainen waardoor het resultaat er heel goed uitziet.

**Code Snippets**

Dit zijn snippets van het JumperAgent.cs script, een component van de agent.

Variabelen:

![image](https://github.com/user-attachments/assets/7f470553-dc95-49e2-a861-76cbb5f9f232)


Start van episode:

![image](https://github.com/user-attachments/assets/89400f0a-6b24-46b5-a0ef-6d3eb922bea5)

We resetten het obstakel naar zijn oorspronkelijke positie en geven het een random snelheid.

Observaties:

![image](https://github.com/user-attachments/assets/bfd6d1ac-1d40-479f-bab0-55232a019dea)


OnActionsReceived:

![image](https://github.com/user-attachments/assets/336f2470-e04a-4b6c-9601-20086588b67e)

Variabelen voor het berekenen van de afstand tussen objecten.
![image](https://github.com/user-attachments/assets/5e2f4932-10bb-4fd7-8444-7af719512d33)

De twee discrete acties die de agent kan uitvoeren zijn springen en niets doen (hij kan enkel springen en punten krijgen door niets te doen als hij op de grond staat).
![image](https://github.com/user-attachments/assets/92784853-9b0b-46e0-8d0c-e66d19109727)

Als het obstakel de muur aanraakt gaat hij net zoals in het begin van elke episode terug naar zijn oorspronkelijke plaats met een random snelheid. Dit is de manier om de meeste punten te krijgen voor de agent.
![image](https://github.com/user-attachments/assets/88ccd830-8893-47f3-a447-a790e0e21f23)

Check of de agent terug op de grond staat (het y-niveau van de grond is 0.5).
![image](https://github.com/user-attachments/assets/90f8b8d7-7d96-490a-a9c4-9088ba10090d)
![image](https://github.com/user-attachments/assets/c6a85ab3-ebcf-4f8c-895f-1ed004692725)

 
De episode eindigt enkel als een obstakel de agent raakt.

ResetAgent:

![image](https://github.com/user-attachments/assets/53e8a780-2645-424f-afec-a6b464432822)
![image](https://github.com/user-attachments/assets/4f564269-d8ec-4f4f-b499-e02f09e58043)

Telkens als de agent de vloer aanraakt gaan we zijn positie en rotatie resetten om te voorkomen dat hij weg van zijn positie springt en op andere plaatsen belandt.

Jump:

![image](https://github.com/user-attachments/assets/6929c457-ad43-4097-a1a1-d8923c79f0fa)

Opwaartse kracht op de agent uitoefenen. 

**Conclusie**

De belangrijkste factor die voor het meeste succes heeft gezorgd in dit project is het niet afstraffen van bepaalde acties en slechts goeie punten toewijzen op de juiste momenten. Het resultaat is een agent die het liefst van al niet geraakt wil worden maar ook graag stil wilt blijven staan waardoor hij enkel op de nodige momenten zal springen.

Link naar het filmpje:
https://ap.cloud.panopto.eu/Panopto/Pages/Viewer.aspx?id=9153b35f-dae9-4f86-b108-b1d301433d11
