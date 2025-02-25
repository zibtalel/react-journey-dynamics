
import { useState, useEffect, useRef } from 'react';
import { ArrowRight, ArrowLeft, Clock, Users, ChefHat } from 'lucide-react';
import { CAROUSEL_ITEMS, STATISTICS, FEATURED_RECIPES } from '../config/data';

const Home = () => {
  const [currentSlide, setCurrentSlide] = useState(0);
  const [statistics, setStatistics] = useState(STATISTICS.map(stat => ({ ...stat, current: 0 })));
  const statsRef = useRef<HTMLDivElement>(null);
  const [isStatsVisible, setIsStatsVisible] = useState(false);
  const recipesRef = useRef<HTMLDivElement>(null);
  const [isRecipesVisible, setIsRecipesVisible] = useState(false);

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

      {/* Statistics Section */}
      <section ref={statsRef} className="py-20 bg-gray-900">
        <div className="container mx-auto px-4">
          <div className="grid grid-cols-2 md:grid-cols-4 gap-8">
            {statistics.map((stat, index) => (
              <div key={index} className="text-center">
                <div className="text-4xl font-bold text-white mb-2">
                  {Math.floor(stat.current)}
                  {stat.suffix}
                </div>
                <div className="text-gray-400">{stat.label}</div>
              </div>
            ))}
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
