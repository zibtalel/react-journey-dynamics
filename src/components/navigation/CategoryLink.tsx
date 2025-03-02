
import React from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { cn } from "@/lib/utils";
import {
  NavigationMenuItem,
  NavigationMenuTrigger,
  NavigationMenuContent,
} from "@/components/ui/navigation-menu";
import { SubItem } from '@/config/menuConfig';

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

  const handleMenuTriggerClick = (e: React.MouseEvent) => {
    if (subItems && subItems.length > 0) {
      e.preventDefault();
    } else {
      navigate(href);
    }
  };

  return (
    <NavigationMenuItem>
      <NavigationMenuTrigger 
        className={cn(
          "h-auto py-2",
          location.pathname === href && "border-2 border-primary rounded-md bg-transparent text-primary"
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
        <NavigationMenuContent className="absolute left-0 w-screen data-[motion^=from-]:animate-in data-[motion^=to-]:animate-out data-[motion^=from-]:fade-in data-[motion^=to-]:fade-out data-[motion=from-end]:slide-in-from-right-52 data-[motion=from-start]:slide-in-from-left-52 data-[motion=to-end]:slide-out-to-right-52 data-[motion=to-start]:slide-out-to-left-52">
          <div className="w-screen flex justify-center">
            <div className="w-[80vw] p-6 bg-white rounded-lg shadow-lg">
              <div className="grid grid-cols-4 gap-6">
                <div className="col-span-3 grid grid-cols-3 gap-6">
                  {subItems.map((item) => (
                    <Link
                      key={item.path}
                      to={item.path}
                      className="block p-4 space-y-2 hover:bg-gray-50 rounded-lg transition-colors"
                    >
                      <div className="aspect-video rounded-lg overflow-hidden bg-gray-100 mb-2">
                        <img 
                          src={item.image} 
                          alt={item.title}
                          className="w-full h-full object-cover"
                        />
                      </div>
                      <h3 className="font-medium text-gray-900">{item.title}</h3>
                      <p className="text-sm text-gray-500">{item.description}</p>
                    </Link>
                  ))}
                </div>
                <div className="col-span-1">
                  <div className="aspect-[3/4] rounded-lg overflow-hidden bg-gray-100 w-full">
                    <img 
                      src={href === "/vetements-cuisine" ? "/VetementDeCuisine/vestedecuisineImagebanner.jpg" : subItems[0].image} 
                      alt={topText}
                      className="w-full h-full object-cover"
                    />
                  </div>
                  <div className="mt-4 text-center">
                    <h3 className="font-medium text-gray-900">{topText}</h3>
                    <p className="text-sm text-gray-500 mt-1">{bottomText}</p>
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
