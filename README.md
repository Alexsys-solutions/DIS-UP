# DIS-UP : Système de Gestion Logistique

DIS-UP est une solution basée sur des microservices pour **digitaliser et automatiser** la logistique chez SONASID, notamment le **flux d’import maritime** de matières premières et leur réception sur site. Cette plateforme couvre la configuration des référentiels, la gestion des arrivages (PortOps), la pesée (Pont à Bascule), le suivi GPS, la finalisation de dossiers d’import, et d’autres fonctionnalités de bout en bout.

---

## Sommaire

1. [Aperçu Général](#aperçu-général)  
2. [Architecture et Microservices](#architecture-et-microservices)  
   - [Microservice de Référentiel](#microservice-de-référentiel)  
   - [Microservice PortOps (Opérations Portuaires)](#microservice-portops-opérations-portuaires)  
   - [Microservice Weighing / Pont à Bascule](#microservice-weighing--pont-à-bascule)  
   - [Microservice Réclamations et Clôture](#microservice-réclamations-et-clôture)  
   - [GPS Tracking et Autres Modules](#gps-tracking-et-autres-modules)  
3. [Flux Fonctionnels Clés](#flux-fonctionnels-clés)  
4. [Services Azure et Événements](#services-azure-et-événements)  
5. [Prise en Main](#prise-en-main)  
6. [Licence](#licence)

---

## 1. Aperçu Général

**DIS-UP** (Digital Integrated Solution for UPstream logistics) vise à :
- Simplifier la **gestion des arrivages** de ferraille depuis l’étranger,
- Assurer un **suivi en temps réel** (notification, tracking GPS, documents),
- Permettre la **pesée automatisée** (pont à bascule),
- Gérer **l’arrivée sur site** et **la réception**,
- Faciliter le **traitement des réclamations** et la **clôture** des dossiers d’import.

Le système s’intègre avec **SAP** pour la récupération des bons de commande et l’envoi/retour de données (ex. : poids, validations), et exploite un **bus d’événements** (Azure Service Bus) pour orchestrer les communications entre microservices.

---

## 2. Architecture et Microservices

DIS-UP suit une **approche microservices** où chaque service a une responsabilité claire :

### Microservice de Référentiel
- Gère toutes les données de référence partagées :  
  - **Fournisseurs**, **Pays**, **Ports**, **Banques**, **Devises**, **Qualités**, etc.  
- Interface d’administration pour créer, modifier ou supprimer les enregistrements référentiels.

### Microservice PortOps (Opérations Portuaires)
- **Cœur du flux d’import** :  
  - Création des arrivages (Arrivage, ArrivageLine pour plusieurs PO),  
  - Nomination du navire, planification de l’arrivée (Laycan, Demurrage, etc.),  
  - Téléchargement des documents (Contrat, Licence, Swift…),  
  - Suivi de la progression jusqu’à la sortie du navire, autorisation de transfert.
- Publie des **événements** (ArrivageCreated, VesselNominated…) vers le bus de messages.

### Microservice Weighing / Pont à Bascule
- Coordonne la **pesée** des camions au port et sur le site :  
  - Ouverture de shift, affectation des conducteurs,  
  - Pesée à vide / chargé, impression des tickets,  
  - Vérifications (surplus, abattement),  
  - Communication temps réel avec PortOpsAPI et SAP.

### Microservice Réclamations et Clôture
- Simplifie la **création** et le **suivi des réclamations** liées à un import,  
- Associe des **documents justificatifs**, intègre la validation Finance,  
- Gère la **clôture définitive** du dossier d’import après résolution du litige.

### GPS Tracking et Autres Modules
- **Module GPS** : Suivi en temps réel des navires (via AIS / Datalastic) et des camions sur la route.  
- **Calcul de Demurrage / Dispatch** : Évalue le dépassement de Laytime (Demurrage) ou la prime pour réduction de temps (Dispatch).  
- Services additionnels (sécurité, analytics, etc.) peuvent s’y greffer via **events**.

---

## 3. Flux Fonctionnels Clés

1. **Paramétrage** : Administration des référentiels, gestion des rôles et habilitations.  
2. **Import Matière Première (PortOps)** :  
   - Enregistrement d’un arrivage en tirant les infos de SAP,  
   - Compléments contractuels (licence, date limite, taxes),  
   - Nomination du navire et validation Supply Chain,  
   - Déchargement et suivi.  
3. **Transfert (Port → Site)** :  
   - Ouverture de shift, chargement camion, pesée départ.  
4. **Réception** :  
   - Pesée à l’arrivée, contrôle du Qualificateur, pesée finale, envoi à SAP.  
5. **Réclamations / Clôture** :  
   - Vérification d’éventuels litiges, ajout de pièces justificatives, finalisation du dossier.

---

## 4. Services Azure et Événements

- **Azure Service Bus** :   
  - Communication asynchrone entre PortOps, Référentiel, Weighing, etc.  
  - Exemples d’événements : `ArrivageCreated`, `TruckWeighed`, `DocumentUploaded`.

- **Azure SQL** :  
  - Base relationnelle pour stocker les entités clés (Arrivage, Vessel, Réclamation…).  
- **Azure Cosmos DB** ou **Azure Blob Storage** :  
  - Historisation des logs, tracking GPS, documents volumineux.

- **Azure DevOps** ou **GitHub Actions** :  
  - CI/CD pour builder et déployer les microservices (Docker, AKS ou App Service).  
- **Azure Monitor / App Insights** :  
  - Logs, alertes et supervision en temps réel.

---

## 5. Prise en Main

1. **Cloner** ce dépôt et ouvrir la solution .NET correspondante.  
2. Configurer les variables (appsettings) pour :  
   - La base SQL ou locale (EF Core migrations),  
   - Les credentials Azure Service Bus (Topics/Queues).  
3. **Démarrer** chaque microservice (Referential, PortOps, Weighing…), vérifier les endpoints Swagger.  
4. Pour tester avec SAP, activer un mock ou connecter au système SAP de préproduction.  
5. Surveiller les logs : l’application publie des événements sur Service Bus pour toute création ou mise à jour critique.

---

## 6. Licence

Ce projet DIS-UP est un outil propriétaire développé pour **SONASID**. Toute redistribution ou utilisation nécessite l’approbation des responsables du projet.  

Pour toute question ou contribution, merci de contacter l’équipe DIS-UP ou la DSI SONASID.

---

**Merci de votre intérêt** pour DIS-UP. Pour plus de détails, veuillez consulter les spécifications fonctionnelles ou contacter les membres de l’équipe projet.
