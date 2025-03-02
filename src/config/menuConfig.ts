export interface MenuItem {
  title: string;
  image: string;
  path: string;
  topText: string;
  bottomText: string;
  subItems: SubItem[];
}

export interface SubItem {
  title: string;
  description: string;
  image: string;
  path: string;
}

export const menuItems: MenuItem[] = [
  {
    title: "Vêtements de cuisine",
    image: "/VetementDeCuisine/vestedecuisineImagebanner.jpg",
    path: "/vetements-cuisine",
    topText: "Vêtements",
    bottomText: "de cuisine",
    subItems: [
      {
        title: "Vestes de Chef",
        description: "Collection premium pour cuisiniers professionnels",
        image: "/VetementDeCuisine/VesteDeChef.jpg",
        path: "/vetements-cuisine/vestes"
      },
      {
        title: "Tabliers",
        description: "Protection et style pour votre cuisine",
        image: "/VetementDeCuisine/TablierDeChef.jpg",
        path: "/vetements-cuisine/tabliers"
      },
      {
        title: "Pantalons",
        description: "Confort et durabilité garantis",
        image: "/VetementDeCuisine/PontalonDeChef.jpg",
        path: "/vetements-cuisine/pantalons"
      },
      {
        title: "Vestes de Boulanger",
        description: "Tenues adaptées à la boulangerie",
        image: "/VetementDeBoulanger&patissier/VesteDeBoulanger.jpg",
        path: "/vetements-cuisine/vestes-boulanger"
      },
      {
        title: "Tabliers Pro",
        description: "Protection maximale pour pâtissiers",
        image: "/VetementDeBoulanger&patissier/TablierDeBoucher.jpg",
        path: "/vetements-cuisine/tabliers-pro"
      },
      {
        title: "Vestes Pro Boucher",
        description: "Confort et hygiène pour la boucherie",
        image: "/VetementDeBoulanger&patissier/VesteProBoucher.jpg",
        path: "/vetements-cuisine/vestes-boucher"
      }
    ]
  },
  {
    title: "Vêtements Service & Hôtellerie",
    image: "/VetementServiceHotellerie/VetementHotelerieService.jpg",
    path: "/vetements-hotellerie",
    topText: "Vêtements",
    bottomText: "Service & Hôtellerie",
    subItems: [
      {
        title: "Uniformes de Service",
        description: "Élégance et confort pour le service",
        image: "/VetementServiceHotellerie/UniformeDeService.jpg",
        path: "/vetements-hotellerie/service"
      },
      {
        title: "Tenues d'Accueil",
        description: "Pour un accueil professionnel",
        image: "/VetementServiceHotellerie/TenueDacceuilHotelBanner.jpg",
        path: "/vetements-hotellerie/accueil"
      }
    ]
  },
  {
    title: "Vêtements de travail",
    image: "/VetementDeTravail/VetementDeTravailBanner.jpg",
    path: "/vetements-travail",
    topText: "Vêtements",
    bottomText: "de travail",
    subItems: [
      {
        title: "Combinaisons",
        description: "Protection intégrale",
        image: "/VetementDeTravail/CombinaionDeTravail.jpg",
        path: "/vetements-travail/combinaisons"
      },
      {
        title: "Vestes de Travail",
        description: "Robustes et fonctionnelles",
        image: "/VetementDeTravail/VesteDeTravail.jpg",
        path: "/vetements-travail/vestes"
      },
      {
        title: "Blouses Médicales",
        description: "Pour les professionnels de santé",
        image: "/VetementDeTravail/BlouseMedical.jpg",
        path: "/vetements-travail/blouses"
      },
      {
        title: "Tuniques Médicales",
        description: "Confort et praticité",
        image: "/VetementDeTravail/TuniqueMedical.png",
        path: "/vetements-travail/tuniques"
      }
    ]
  },
  {
    title: "Chaussures de sécurité",
    image: "/ChausureDeTravail/ChaussureDeTravail.png",
    path: "/chaussures",
    topText: "Chaussures",
    bottomText: "de sécurité",
    subItems: [
      {
        title: "Chaussures Cuisine",
        description: "Sécurité en cuisine",
        image: "/ChausureDeTravail/ChaussureDeCuisine.jpg",
        path: "/chaussures/cuisine"
      },
      {
        title: "Chaussures Pro",
        description: "Protection renforcée",
        image: "/ChausureDeTravail/ChaussureDeTravailBanner.jpg",
        path: "/chaussures/industrie"
      }
    ]
  },
  {
    title: "Nos packs Complet",
    image: "/Packs/PacksBanner.jpg",
    path: "/nos-packs",
    topText: "Nos",
    bottomText: "packs Complet",
    subItems: [
      {
        title: "Pack Restaurant",
        description: "Solution complète pour restaurant",
        image: "/Packs/PackRestaurant.jpg",
        path: "/nos-packs/restaurant"
      },
      {
        title: "Pack Café",
        description: "Équipement pour café et brasserie",
        image: "/Packs/PackCafe.jpg",
        path: "/nos-packs/cafe"
      },
      {
        title: "Pack Hôtel",
        description: "Tout pour votre établissement hôtelier",
        image: "/Packs/PackHotel.jpg",
        path: "/nos-packs/hotel"
      },
      {
        title: "Pack Médecin",
        description: "Équipement complet pour professionnels de santé",
        image: "/Packs/PackMedecin.jpg",
        path: "/nos-packs/medecin"
      }
    ]
  },
  {
    title: "Produits Personalisable",
    image: "/ProduitsMarketing/ProduitMarketingBanner.jpg",
    path: "/produits-marketing",
    topText: "Produits",
    bottomText: "Personalisable",
    subItems: [
      {
        title: "Drapeaux",
        description: "Drapeaux publicitaires personnalisables",
        image: "/ProduitsMarketing/DrapeauMarketing.jpg",
        path: "/produits-marketing/drapeaux"
      },
      {
        title: "Mugs",
        description: "Mugs personnalisés pour votre communication",
        image: "/ProduitsMarketing/MugsPersonalise.jpg",
        path: "/produits-marketing/mugs"
      },
      {
        title: "Carnets",
        description: "Carnets et bloc-notes personnalisés",
        image: "/ProduitsMarketing/CarnetPeronalise.jpg",
        path: "/produits-marketing/carnets"
      }
    ]
  }
];
