import { useState, useEffect, useRef } from 'react';
import { ArrowRight, ArrowLeft, Clock, Users, ChefHat, Award, Globe, Truck, Factory } from 'lucide-react';
import { CAROUSEL_ITEMS, STATISTICS, FEATURED_RECIPES } from '../config/data';

const Home = () => {
  const [currentSlide, setCurrentSlide] = useState(0);
  const [statistics, setStatistics] = useState(STATISTICS.map(stat => ({ ...stat, current: 0 })));
  const statsRef = useRef<HTMLDivElement>(null);
  const [isStatsVisible, setIsStatsVisible] = useState(false);
  const recipesRef = useRef<HTMLDivElement>(null);

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

  return (
    <main className="flex-grow">
      {/* Carousel Section */}
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
                  <h1 className="text-4xl md:text-6xl font-playfair mb-6">{item.title}</h1>
                  <p className="text-xl mb-8">{item.description}</p>
                </div>
              </div>
            </div>
          </div>
        ))}

        {/* Carousel Controls */}
        <div className="absolute bottom-8 left-8 flex items-center space-x-4">
          <button onClick={prevSlide} className="p-2 bg-white/20 rounded-full">
            <ArrowLeft className="text-white" />
          </button>
          <button onClick={nextSlide} className="p-2 bg-white/20 rounded-full">
            <ArrowRight className="text-white" />
          </button>
        </div>
      </section>

      {/* About Section */}
      <section className="py-24 bg-gradient-to-b from-white to-gray-50">
        <div className="container mx-auto px-4">
          <div className="flex flex-col lg:flex-row items-center gap-12">
            {/* Logo Side */}
            <div className="lg:w-1/2 relative group">
              <div className="absolute -inset-4 bg-gradient-to-r from-[#96cc39]/30 to-[#64381b]/30 rounded-xl blur-xl opacity-75 group-hover:opacity-100 transition duration-500" />
              <div className="relative">
                <div className="rounded-xl overflow-hidden shadow-2xl transform group-hover:scale-[1.02] transition duration-500">
                  <img
                    src="https://placehold.co/800x600/96cc39/ffffff?text=Notre+Histoire"
                    alt="About Us"
                    className="w-full h-auto"
                  />
                </div>
                <div className="absolute -bottom-6 -right-6 bg-white p-4 rounded-lg shadow-xl">
                  <img
                    src="https://i.ibb.co/Rp6QnpS/logo.webp"
                    alt="Company Logo"
                    className="w-24 h-24 object-contain"
                  />
                </div>
              </div>
            </div>

            {/* Content Side */}
            <div className="lg:w-1/2 space-y-8">
              <div>
                <h4 className="text-[#96cc39] font-medium mb-2">Notre Histoire</h4>
                <h2 className="text-4xl font-playfair font-bold mb-6 text-gray-900">
                  L'Excellence des Dattes <span className="text-[#64381b]">Premium</span>
                </h2>
                <div className="w-20 h-1 bg-gradient-to-r from-[#96cc39] to-[#64381b] rounded-full mb-6" />
                <p className="text-gray-600 leading-relaxed mb-6">
                  Depuis plus de deux décennies, nous nous engageons à fournir les meilleures dattes 
                  de Tunisie aux quatre coins du monde. Notre passion pour la qualité et notre 
                  expertise dans la sélection des meilleures variétés nous ont permis de devenir 
                  un leader dans l'exportation de dattes premium.
                </p>
                <p className="text-gray-600 leading-relaxed">
                  Chaque datte que nous sélectionnons raconte une histoire de tradition, 
                  de qualité et d'excellence. Notre engagement envers la durabilité et 
                  l'innovation nous permet de maintenir les plus hauts standards de qualité.
                </p>
              </div>

              {/* Statistics in About Section */}
              <div ref={statsRef} className="grid grid-cols-2 md:grid-cols-4 gap-6 pt-8 border-t border-gray-200">
                {statistics.map((stat, index) => (
                  <div key={index} className="text-center group">
                    <div className="mb-3">
                      {index === 0 && <Globe className="w-8 h-8 mx-auto text-[#96cc39] group-hover:scale-110 transition-transform" />}
                      {index === 1 && <Truck className="w-8 h-8 mx-auto text-[#96cc39] group-hover:scale-110 transition-transform" />}
                      {index === 2 && <Award className="w-8 h-8 mx-auto text-[#96cc39] group-hover:scale-110 transition-transform" />}
                      {index === 3 && <Factory className="w-8 h-8 mx-auto text-[#96cc39] group-hover:scale-110 transition-transform" />}
                    </div>
                    <div className="text-3xl font-bold text-[#64381b] mb-1">
                      {Math.floor(stat.current)}
                      {stat.suffix}
                    </div>
                    <div className="text-sm text-gray-500">{stat.label}</div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Recipes Section */}
      <section ref={recipesRef} className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl font-bold text-center mb-12">Nos Recettes</h2>
          <div className="grid md:grid-cols-3 gap-8">
            {FEATURED_RECIPES.map((recipe, index) => (
              <div
                key={index}
                className="bg-white rounded-lg shadow-lg overflow-hidden transform transition-all duration-300 hover:-translate-y-2"
              >
                <div className="relative h-48">
                  <img
                    src={recipe.image}
                    alt={recipe.title}
                    className="w-full h-full object-cover"
                  />
                </div>
                <div className="p-6">
                  <h3 className="text-xl font-semibold mb-2">{recipe.title}</h3>
                  <p className="text-gray-600 mb-4">{recipe.description}</p>
                  <div className="flex items-center justify-between text-sm text-gray-500">
                    <div className="flex items-center">
                      <Clock className="w-4 h-4 mr-1" />
                      <span>{recipe.time}</span>
                    </div>
                    <div className="flex items-center">
                      <Users className="w-4 h-4 mr-1" />
                      <span>{recipe.servings} pers.</span>
                    </div>
                    <div className="flex items-center">
                      <ChefHat className="w-4 h-4 mr-1" />
                      <span>{recipe.difficulty}</span>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </main>
  );
};

export default Home;
