import React, { useState, useEffect, useRef } from 'react';
import { PreloadingScreen } from './components/PreloadingScreen';
import { ClientTypeModal } from './components/ClientTypeModal';
import Navbar from './components/Navbar';
import { Footer } from './components/Footer';
import { Contact } from './pages/Contact';
import type { ClientType } from './types';
import { ArrowRight, Star, ArrowLeft, ArrowRight as ArrowRightIcon, Clock, Users, ChefHat } from 'lucide-react';

// Previous carousel and statistics constants remain the same...
const CAROUSEL_ITEMS = [
  {
    image: 'https://placehold.co/1920x1080/96cc39/ffffff?text=Premium+Dates',
    title: 'Découvrez l\'Excellence des Dattes Premium',
    description: 'Une sélection exceptionnelle de dattes de la plus haute qualité, cultivées avec passion.'
  },
  {
    image: 'https://placehold.co/1920x1080/64381b/ffffff?text=Qualité+Supérieure',
    title: 'Qualité Supérieure Garantie',
    description: 'Des dattes soigneusement sélectionnées pour garantir une expérience gustative unique.'
  },
  {
    image: 'https://placehold.co/1920x1080/96cc39/ffffff?text=Exportation+Mondiale',
    title: 'Exportation Dans Le Monde Entier',
    description: 'Nous livrons nos produits d\'exception aux quatre coins du monde.'
  }
];

const STATISTICS = [
  { label: 'Clients Mondiaux', value: 1500, suffix: '+' },
  { label: 'Pays d\'Exportation', value: 45, suffix: '' },
  { label: 'Tonnes de Dattes Exportées', value: 2500, suffix: 'T' },
  { label: 'Producteurs Affiliés', value: 200, suffix: '+' }
];

const FEATURED_RECIPES = [
  {
    title: "Gâteau Moelleux aux Dattes",
    image: "https://images.unsplash.com/photo-1605291525368-244376c0b5ee?auto=format&fit=crop&q=80&w=800",
    time: "45 min",
    servings: 8,
    difficulty: "Facile",
    description: "Un délicieux gâteau moelleux aux dattes, parfait pour le goûter ou le dessert. Une recette traditionnelle revisitée avec nos dattes premium.",
    category: "Desserts"
  },
  {
    title: "Smoothie Énergétique aux Dattes",
    image: "https://images.unsplash.com/photo-1623065422902-30a2d299bbe4?auto=format&fit=crop&q=80&w=800",
    time: "10 min",
    servings: 2,
    difficulty: "Facile",
    description: "Un smoothie sain et énergisant, préparé avec nos dattes premium, du lait d'amande et une touche de cannelle.",
    category: "Boissons"
  },
  {
    title: "Barres Énergétiques Maison",
    image: "https://images.unsplash.com/photo-1582998634272-7340ca75b1eb?auto=format&fit=crop&q=80&w=800",
    time: "20 min",
    servings: 12,
    difficulty: "Moyen",
    description: "Des barres énergétiques saines et naturelles, parfaites pour les sportifs et les gourmands en quête d'énergie.",
    category: "Snacks"
  }
];

function App() {
  // Previous state declarations remain the same...
  const [isLoading, setIsLoading] = useState(true);
  const [clientType, setClientType] = useState<ClientType>(null);
  const [currentPage, setCurrentPage] = useState('home');
  const [currentSlide, setCurrentSlide] = useState(0);
  const [statistics, setStatistics] = useState(STATISTICS.map(stat => ({ ...stat, current: 0 })));
  const statsRef = useRef<HTMLDivElement>(null);
  const [isStatsVisible, setIsStatsVisible] = useState(false);
  const recipesRef = useRef<HTMLDivElement>(null);
  const [isRecipesVisible, setIsRecipesVisible] = useState(false);

  // Previous useEffect hooks remain the same...
  useEffect(() => {
    const timer = setTimeout(() => {
      setIsLoading(false);
    }, 2000);

    return () => clearTimeout(timer);
  }, []);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentSlide((prev) => (prev + 1) % CAROUSEL_ITEMS.length);
    }, 5000);

    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          setIsStatsVisible(true);
        }
      },
      { threshold: 0.5 }
    );

    if (statsRef.current) {
      observer.observe(statsRef.current);
    }

    return () => observer.disconnect();
  }, []);

  // New observer for recipes section
  useEffect(() => {
    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          setIsRecipesVisible(true);
        }
      },
      { threshold: 0.2 }
    );

    if (recipesRef.current) {
      observer.observe(recipesRef.current);
    }

    return () => observer.disconnect();
  }, []);

  useEffect(() => {
    if (isStatsVisible) {
      statistics.forEach((stat, index) => {
        const steps = 50;
        const increment = stat.value / steps;
        let current = 0;
        
        const interval = setInterval(() => {
          current += increment;
          if (current >= stat.value) {
            current = stat.value;
            clearInterval(interval);
          }
          
          setStatistics(prev => 
            prev.map((s, i) => 
              i === index ? { ...s, current: Math.floor(current) } : s
            )
          );
        }, 40);

        return () => clearInterval(interval);
      });
    }
  }, [isStatsVisible]);

  const nextSlide = () => {
    setCurrentSlide((prev) => (prev + 1) % CAROUSEL_ITEMS.length);
  };

  const prevSlide = () => {
    setCurrentSlide((prev) => (prev - 1 + CAROUSEL_ITEMS.length) % CAROUSEL_ITEMS.length);
  };

  if (isLoading) {
    return <PreloadingScreen />;
  }

  const renderPage = () => {
    switch (currentPage) {
      case 'contact':
        return <Contact />;
      default:
        return (
          <main className="flex-grow">
            {/* Previous sections (Carousel, About, Statistics) remain the same... */}
            <section className="relative h-screen">
              {CAROUSEL_ITEMS.map((item, index) => (
                <div
                  key={index}
                  className={`absolute inset-0 transition-opacity duration-1000 ${
                    currentSlide === index ? 'opacity-100' : 'opacity-0'
                  }`}
                  style={{
                    backgroundImage: `url(${item.image})`,
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                  }}
                >
                  <div className="absolute inset-0 bg-black/40" />
                  <div className="absolute inset-0 flex items-center justify-center">
                    <div className="container mx-auto px-4">
                      <div className="max-w-3xl mx-auto text-center text-white">
                        <h1 className="text-4xl md:text-6xl font-playfair mb-6 transform transition-all duration-1000 delay-300"
                            style={{
                              opacity: currentSlide === index ? 1 : 0,
                              transform: currentSlide === index ? 'translateY(0)' : 'translateY(20px)'
                            }}>
                          {item.title}
                        </h1>
                        <p className="text-xl mb-8 transform transition-all duration-1000 delay-500"
                           style={{
                             opacity: currentSlide === index ? 1 : 0,
                             transform: currentSlide === index ? 'translateY(0)' : 'translateY(20px)'
                           }}>
                          {item.description}
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
              ))}

              {/* Carousel Controls */}
              <div className="absolute bottom-8 left-8 flex items-center space-x-4">
                <div className="bg-white/10 backdrop-blur-sm rounded-full p-4">
                  <div className="flex items-center space-x-2">
                    <span className="text-white font-medium">
                      {String(currentSlide + 1).padStart(2, '0')}
                    </span>
                    <div className="w-10 h-0.5 bg-white/50">
                      <div
                        className="h-full bg-white transition-all duration-5000 origin-left"
                        style={{ width: `${((currentSlide + 1) / CAROUSEL_ITEMS.length) * 100}%` }}
                      />
                    </div>
                    <span className="text-white/50 font-medium">
                      {String(CAROUSEL_ITEMS.length).padStart(2, '0')}
                    </span>
                  </div>
                </div>
                
                <div className="flex space-x-2">
                  <button
                    onClick={prevSlide}
                    className="w-10 h-10 rounded-full bg-white/10 backdrop-blur-sm flex items-center justify-center text-white hover:bg-white/20 transition-colors"
                  >
                    <ArrowLeft size={20} />
                  </button>
                  <button
                    onClick={nextSlide}
                    className="w-10 h-10 rounded-full bg-white/10 backdrop-blur-sm flex items-center justify-center text-white hover:bg-white/20 transition-colors"
                  >
                    <ArrowRightIcon size={20} />
                  </button>
                </div>
              </div>
            </section>

            {/* About Section */}
            <section className="py-20 bg-white">
              <div className="container mx-auto px-4">
                <div className="grid md:grid-cols-2 gap-12 items-center">
                  <div className="relative">
                    <div className="w-full h-[400px] rounded-2xl overflow-hidden">
                      <img
                        src="https://i.ibb.co/Rp6QnpSt/logo.webp"
                        alt="À Propos de Premium Dates"
                        className="w-full h-full object-cover"
                      />
                    </div>
                    <div className="absolute -bottom-6 -right-6 w-48 h-48 bg-[#96cc39]/10 rounded-2xl -z-10" />
                    <div className="absolute -top-6 -left-6 w-48 h-48 bg-[#64381b]/10 rounded-2xl -z-10" />
                  </div>

                  <div>
                    <h2 className="text-4xl font-playfair mb-6 relative inline-block">
                      À Propos de Premium Dates
                      <span className="absolute bottom-0 left-0 w-1/2 h-0.5 bg-[#96cc39]" />
                    </h2>
                    <p className="text-gray-600 mb-8 leading-relaxed">
                      Depuis notre création, Premium Dates s'est engagé à fournir les meilleures dattes 
                      au monde. Notre passion pour la qualité et notre engagement envers l'excellence 
                      nous ont permis de devenir un leader mondial dans l'exportation de dattes premium.
                    </p>
                    <p className="text-gray-600 mb-8 leading-relaxed">
                      Nous travaillons en étroite collaboration avec des producteurs locaux sélectionnés 
                      pour garantir les plus hauts standards de qualité. Notre réseau mondial de 
                      distribution nous permet de livrer nos produits d'exception aux quatre coins du monde.
                    </p>
                  </div>
                </div>

                {/* Statistics */}
                <div
                  ref={statsRef}
                  className="grid grid-cols-2 md:grid-cols-4 gap-8 mt-20"
                >
                  {statistics.map((stat, index) => (
                    <div
                      key={index}
                      className="text-center transform transition-all duration-700"
                      style={{
                        opacity: isStatsVisible ? 1 : 0,
                        transform: isStatsVisible
                          ? 'translateY(0)'
                          : 'translateY(20px)',
                        transitionDelay: `${index * 200}ms`,
                      }}
                    >
                      <div className="text-4xl font-playfair text-[#96cc39] mb-2">
                        {Math.floor(stat.current)}
                        {stat.suffix}
                      </div>
                      <div className="text-gray-600">{stat.label}</div>
                    </div>
                  ))}
                </div>
              </div>
            </section>

            {/* Recipes Section */}
            <section className="py-20 bg-gradient-to-b from-white to-gray-50">
              <div className="container mx-auto px-4">
                <div className="text-center mb-16">
                  <h2 className="text-4xl font-playfair mb-6 relative inline-block">
                    Nos Recettes Gourmandes
                    <span className="absolute bottom-0 left-0 w-1/2 h-0.5 bg-[#96cc39]" />
                  </h2>
                  <p className="text-gray-600 max-w-2xl mx-auto">
                    Découvrez nos délicieuses recettes mettant en valeur nos dattes premium. 
                    Des créations uniques pour sublimer vos moments gourmands.
                  </p>
                </div>

                <div
                  ref={recipesRef}
                  className="grid md:grid-cols-3 gap-8"
                >
                  {FEATURED_RECIPES.map((recipe, index) => (
                    <div
                      key={index}
                      className="group relative bg-white rounded-2xl overflow-hidden shadow-lg transform transition-all duration-500 hover:-translate-y-2 hover:shadow-xl"
                      style={{
                        opacity: isRecipesVisible ? 1 : 0,
                        transform: `translateY(${isRecipesVisible ? '0' : '40px'})`,
                        transitionDelay: `${index * 200}ms`,
                      }}
                    >
                      <div className="relative h-64 overflow-hidden">
                        <img
                          src={recipe.image}
                          alt={recipe.title}
                          className="w-full h-full object-cover transform transition-transform duration-500 group-hover:scale-110"
                        />
                        <div className="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent" />
                        <div className="absolute bottom-4 left-4 right-4">
                          <span className="inline-block px-3 py-1 bg-[#96cc39] text-white text-sm rounded-full mb-2">
                            {recipe.category}
                          </span>
                          <h3 className="text-xl font-playfair text-white">
                            {recipe.title}
                          </h3>
                        </div>
                      </div>
                      
                      <div className="p-6">
                        <div className="flex items-center justify-between mb-4 text-sm text-gray-600">
                          <div className="flex items-center space-x-2">
                            <Clock size={16} className="text-[#96cc39]" />
                            <span>{recipe.time}</span>
                          </div>
                          <div className="flex items-center space-x-2">
                            <Users size={16} className="text-[#96cc39]" />
                            <span>{recipe.servings} pers.</span>
                          </div>
                          <div className="flex items-center space-x-2">
                            <ChefHat size={16} className="text-[#96cc39]" />
                            <span>{recipe.difficulty}</span>
                          </div>
                        </div>
                        
                        <p className="text-gray-600 mb-4 line-clamp-2">
                          {recipe.description}
                        </p>
                        
                        <button className="flex items-center text-[#96cc39] font-medium group">
                          <span>Voir la recette</span>
                          <ArrowRight size={16} className="ml-2 transform transition-transform group-hover:translate-x-1" />
                        </button>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </section>
          </main>
        );
    }
  };

  return (
    <div className="min-h-screen flex flex-col">
      {clientType === null ? (
        <ClientTypeModal onSelect={setClientType} />
      ) : (
        <>
          <Navbar clientType={clientType} onPageChange={setCurrentPage} currentPage={currentPage} />
          {renderPage()}
          <Footer />
        </>
      )}
    </div>
  );
}

export default App;