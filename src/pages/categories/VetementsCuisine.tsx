
import { useState } from "react";
import { Link } from "react-router-dom";
import { ChevronRight, Filter } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { products } from "@/config/products";
import ProductCard from "@/components/products/ProductCard";
import FeaturesSection from "@/components/features/FeaturesSection";

const VetementsCuisine = () => {
  const [sortBy, setSortBy] = useState("recommended");
  const cuisineProducts = products.filter(p => p.category === 'vetements-cuisine');

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Breadcrumb */}
      <div className="bg-white shadow-sm">
        <div className="container mx-auto py-3 px-4">
          <div className="flex items-center gap-2 text-sm text-gray-600">
            <Link to="/" className="hover:text-primary">Accueil</Link>
            <ChevronRight className="h-4 w-4" />
            <span className="text-gray-900 font-medium">Vêtements de Cuisine</span>
          </div>
        </div>
      </div>

      {/* Hero Section */}
      <div className="relative h-[400px] md:h-[500px] overflow-hidden">
        <img
          src="/lovable-uploads/98a68746-eff6-4ad1-b7d9-7fed922db14f.png"
          alt="Vêtements de Cuisine"
          className="w-full h-full object-cover"
        />
        <div className="absolute inset-0 bg-gradient-to-r from-black/60 to-black/40" />
        <div className="absolute inset-0 flex items-center justify-center p-4">
          <div className="text-center text-white max-w-4xl mx-auto px-4">
            <h1 className="text-4xl md:text-6xl font-bold mb-6">Vêtements de Cuisine Professionnels</h1>
            <p className="text-lg md:text-xl opacity-90 mb-8 max-w-2xl mx-auto">
              Des vêtements de cuisine de haute qualité pour les professionnels exigeants. Confort, durabilité et style pour votre cuisine.
            </p>
            <Button asChild className="bg-white text-primary hover:bg-gray-100">
              <Link to="#products">Découvrir la collection</Link>
            </Button>
          </div>
        </div>
      </div>

      {/* Features Section */}
      <FeaturesSection />

      {/* Products Section */}
      <section id="products" className="py-16">
        <div className="container mx-auto px-4">
          <div className="max-w-7xl mx-auto">
            {/* Sorting Section */}
            <div className="flex items-center justify-between mb-8">
              <h2 className="text-2xl font-bold">Nos Produits</h2>
              <div className="flex items-center gap-4">
                <Filter className="h-5 w-5 text-gray-500" />
                <Select value={sortBy} onValueChange={setSortBy}>
                  <SelectTrigger className="w-[180px]">
                    <SelectValue placeholder="Trier par" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="recommended">Recommandés</SelectItem>
                    <SelectItem value="price-asc">Prix croissant</SelectItem>
                    <SelectItem value="price-desc">Prix décroissant</SelectItem>
                    <SelectItem value="newest">Plus récents</SelectItem>
                  </SelectContent>
                </Select>
              </div>
            </div>

            {/* Products Grid */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
              {cuisineProducts.map((product) => (
                <ProductCard
                  key={product.id}
                  id={product.id}
                  name={product.name}
                  description={product.description}
                  price={product.startingPrice}
                  image={product.image || '/placeholder.png'}
                />
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Newsletter Section */}
      <section className="bg-primary text-white py-16">
        <div className="container mx-auto px-4">
          <div className="max-w-2xl mx-auto text-center">
            <h2 className="text-3xl font-bold mb-4">Restez informé</h2>
            <p className="mb-8">Inscrivez-vous à notre newsletter pour recevoir nos dernières offres et nouveautés.</p>
            <div className="flex gap-4 max-w-md mx-auto">
              <input
                type="email"
                placeholder="Votre email"
                className="flex-1 px-4 py-2 rounded-lg text-gray-900"
              />
              <Button className="bg-white text-primary hover:bg-gray-100">
                S'inscrire
              </Button>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default VetementsCuisine;
