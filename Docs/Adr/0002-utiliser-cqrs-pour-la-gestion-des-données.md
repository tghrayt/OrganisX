# Utiliser CQRS pour la gestion des données 

## Status

* Status: accepted  <!-- optional -->

## [short title of solved problem and solution]

* Deciders: tghrayt <!-- optional -->
* Date: 13/09/2025

Technical Story: [description | ticket/issue URL] <!-- optional -->

## Context and Problem Statement

Le projet doit gérer un volume important d’opérations de lecture et d’écriture.  
Je dois gérer un volume important d’opérations de lecture et d’écriture.  
Les règles métiers sont complexes côté écriture, tandis que les requêtes de lecture doivent être rapides et optimisées pour l’affichage dans l’application Angular.  
L’approche traditionnelle (CRUD direct via une même couche) rend le code difficile à maintenir et à faire évoluer.

## Decision Drivers <!-- optional -->

J’ai décidé d’adopter **CQRS (Command Query Responsibility Segregation)** afin de séparer clairement :
- Les **commandes (Command)** qui modifient l’état du système.  
- Les **requêtes (Query)** qui lisent les données.  

## Considered Options

* J’utiliserai **MediatR** pour gérer les Command/Query via un pattern mediator.

## Decision Outcome

- **CRUD classique** : plus simple à mettre en place, mais conduit à un code fortement couplé et difficilement testable.  
- **Event Sourcing** : trop complexe pour la taille actuelle du projet et non nécessaire dans l’immédiat.  

### Positive Consequences <!-- optional -->

* ✅ Meilleure lisibilité et séparation des responsabilités.  
* ✅ Facilite les tests unitaires (les handlers sont indépendants).  
* ✅ Évolutivité : possibilité d’optimiser différemment les lectures (ex. vues matérialisées, projections).  

### Negative Consequences <!-- optional -->

* ❌ Ajoute de la complexité et nécessite une discipline stricte dans la structuration du projet.  
* ❌ Multiplication du nombre de classes (Command, Query, Handlers).
