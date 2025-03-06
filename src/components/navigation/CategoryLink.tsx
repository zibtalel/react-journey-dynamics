
import React from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { cn } from "@/lib/utils";
import {
  NavigationMenuItem,
  NavigationMenuTrigger,
  NavigationMenuContent,
} from "@/components/ui/navigation-menu";
import { SubItem } from '@/config/menuConfig';
import { Check, ChevronRight } from 'lucide-react';
import Image from '@/components/ui/image';
import { getCategoryBanner } from '@/utils/categoryImageMappings';

interface CategoryLinkProps {
  href: string;
  topText: string;
  bottomText: string;
  subItems?: SubItem[];
}

const CategoryLink: React.FC<CategoryLinkProps> = ({ 
  href, 
  topText, 
  bottomText, 
  subItems 
}) => {
  const navigate = useNavigate();
  const location = useLocation();
  const isActive = location.pathname === href || location.pathname.startsWith(href + '/');
  const bannerImage = getCategoryBanner(href);

  const handleMenuTriggerClick = (e: React.MouseEvent) => {
    if (!subItems || subItems.length === 0) {
      navigate(href);
    }
  };

  return (
    <NavigationMenuItem>
      <NavigationMenuTrigger 
        className={cn(
          "h-auto py-1.5 transition-all duration-300", // Reduced padding from py-2 to py-1.5
          isActive 
            ? "border-2 border-primary rounded-md bg-transparent text-primary shadow-sm" 
            : "hover:bg-gray-50"
        )} 
        onClick={handleMenuTriggerClick}
      >
        <div className="flex flex-col text-left min-w-max px-3 rounded-md transition-all">
          <span className="text-sm font-medium text-gray-800 whitespace-nowrap">
            {topText}
          </span>
          <span className="text-xs text-gray-600 whitespace-nowrap">
            {bottomText}
          </span>
        </div>
      </NavigationMenuTrigger>
      {subItems && (
        <NavigationMenuContent 
          className="z-50 absolute left-0 w-screen animate-in fade-in-50 data-[side=bottom]:slide-in-from-top-2 data-[side=top]:slide-in-from-bottom-2 duration-300"
        >
          <div className="flex justify-center py-3 w-full backdrop-blur-sm bg-white/90"> {/* Reduced py-4 to py-3 */}
            <div className="w-[95vw] lg:w-[85vw] xl:w-[80vw] p-3 lg:p-5 bg-white rounded-lg shadow-xl border border-gray-100 animate-fade-in"> {/* Reduced p-4 to p-3 and lg:p-6 to lg:p-5 */}
              <div className="grid grid-cols-1 md:grid-cols-4 gap-3 lg:gap-5"> {/* Reduced gap-4 to gap-3 and lg:gap-6 to lg:gap-5 */}
                <div className="col-span-1 md:col-span-3 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-2.5 lg:gap-3.5"> {/* Reduced gap-3 to gap-2.5 and lg:gap-4 to lg:gap-3.5 */}
                  {subItems.map((item) => {
                    const isItemActive = location.pathname === item.path;
                    return (
                      <Link
                        key={item.path}
                        to={item.path}
                        className={cn(
                          "group block p-1.5 lg:p-2.5 space-y-1 rounded-lg transition-all duration-300", /* Reduced p-2 to p-1.5 and lg:p-3 to lg:p-2.5 */
                          isItemActive 
                            ? "bg-gray-50 shadow-sm" 
                            : "hover:bg-gray-50 hover:shadow-md"
                        )}
                      >
                        <div className="aspect-square rounded-lg overflow-hidden bg-gray-100 mb-1.5 shadow-sm group-hover:shadow-md transition-shadow relative w-[75%] mx-auto">
                          <Image 
                            src={item.image} 
                            alt={item.title}
                            className="w-full h-full object-contain p-2"
                          />
                          {isItemActive && (
                            <div className="absolute top-2 right-2 bg-primary text-white p-1.5 rounded-full">
                              <Check className="h-3 w-3" />
                            </div>
                          )}
                        </div>
                        <h3 className={cn(
                          "font-medium text-xs group-hover:text-primary transition-colors truncate",
                          isItemActive ? "text-primary" : "text-gray-900"
                        )}>
                          {item.title}
                        </h3>
                        <p className="text-xs text-gray-500 line-clamp-2 text-[10px]">{item.description}</p>
                        {isItemActive && (
                          <div className="flex items-center text-primary text-xs">
                            <Check className="h-3 w-3 mr-1" /> Sélectionné
                          </div>
                        )}
                      </Link>
                    );
                  })}
                </div>
                <div className="col-span-1 flex flex-col items-center md:items-start">
                  <div className="aspect-[2/3] rounded-lg overflow-hidden bg-gray-100 w-full max-w-[240px] md:max-w-none shadow-md hover:shadow-lg transition-shadow h-[380px]"> {/* Reduced h-[450px] to h-[380px] (reduced by ~15%) */}
                    <Image 
                      src={bannerImage} 
                      alt={topText}
                      className="w-full h-full object-cover hover:scale-105 transition-transform duration-300"
                    />
                  </div>
                  <div className="mt-2.5 lg:mt-3.5 text-center md:text-left"> {/* Reduced mt-3 to mt-2.5 and lg:mt-4 to lg:mt-3.5 */}
                    <h3 className="font-medium text-sm lg:text-base text-gray-900">{topText}</h3>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </NavigationMenuContent>
      )}
    </NavigationMenuItem>
  );
};

export default CategoryLink;
