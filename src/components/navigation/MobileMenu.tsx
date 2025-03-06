
import React from 'react';
import { MenuItem } from '@/config/menuConfig';
import { Button } from "@/components/ui/button";
import { Menu, ChevronRight, ArrowLeft } from "lucide-react";
import { 
  Sheet, 
  SheetContent, 
  SheetHeader, 
  SheetTitle, 
  SheetTrigger 
} from "@/components/ui/sheet";
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { cn } from "@/lib/utils";

interface MobileMenuProps {
  menuItems: MenuItem[];
  activeMenuItem: MenuItem | null;
  setActiveMenuItem: (item: MenuItem | null) => void;
  isSubmenuOpen: boolean;
  setIsSubmenuOpen: (isOpen: boolean) => void;
  handleNavigation: (path: string) => void;
}

const MobileMenu: React.FC<MobileMenuProps> = ({
  menuItems,
  activeMenuItem,
  setActiveMenuItem,
  isSubmenuOpen,
  setIsSubmenuOpen,
  handleNavigation
}) => {
  const location = useLocation();
  
  const openSubmenu = (item: MenuItem) => {
    setActiveMenuItem(item);
    setIsSubmenuOpen(true);
  };

  return (
    <>
      <Sheet>
        <SheetTrigger asChild>
          <Button variant="ghost" size="icon" className="md:hidden">
            <Menu className="h-6 w-6" />
          </Button>
        </SheetTrigger>
        <SheetContent side="left" className="p-0 w-[300px]">
          <SheetHeader className="p-4 border-b">
            <SheetTitle className="text-left">Menu</SheetTitle>
          </SheetHeader>
          
          <div className="bg-white p-4 flex items-center justify-center border-b">
            <img src="/logo.png" alt="ELLES" className="h-12" />
          </div>

          <div className="divide-y">
            {menuItems.map((item, index) => (
              <button
                key={index}
                className={cn(
                  "w-full flex items-center justify-between p-4 hover:bg-gray-50",
                  location.pathname === item.path && "border-2 border-primary text-primary rounded-md"
                )}
                onClick={() => {
                  if (item.subItems && item.subItems.length > 0) {
                    openSubmenu(item);
                  } else {
                    handleNavigation(item.path);
                  }
                }}
              >
                <div className="flex items-center gap-3">
                  <div className="w-10 h-10 rounded-full overflow-hidden">
                    <img
                      src={item.image}
                      alt={item.title}
                      className="w-full h-full object-cover"
                    />
                  </div>
                  <span className="text-sm font-medium text-left">{item.title}</span>
                </div>
                <ChevronRight className="h-5 w-5 text-gray-400" />
              </button>
            ))}
          </div>
        </SheetContent>
      </Sheet>
      
      <Sheet open={isSubmenuOpen} onOpenChange={setIsSubmenuOpen}>
        <SheetContent side="left" className="w-full sm:w-[350px] p-0">
          {activeMenuItem && (
            <div className="flex flex-col h-full">
              <SheetHeader className="p-4 border-b">
                <Button 
                  variant="ghost" 
                  className="flex items-center gap-2 -ml-2"
                  onClick={() => setIsSubmenuOpen(false)}
                >
                  <ArrowLeft className="h-5 w-5" />
                  <span>Retour</span>
                </Button>
                <SheetTitle className="mt-2">{activeMenuItem.title}</SheetTitle>
              </SheetHeader>
              <div className="flex-1 overflow-y-auto p-4 space-y-4">
                {activeMenuItem.subItems.map((subItem) => (
                  <div
                    key={subItem.path}
                    className="group cursor-pointer"
                    onClick={() => handleNavigation(subItem.path)}
                  >
                    <div className="aspect-video rounded-lg overflow-hidden bg-gray-100 mb-3">
                      <img 
                        src={subItem.image} 
                        alt={subItem.title}
                        className="w-full h-full object-cover transition-transform group-hover:scale-105"
                      />
                    </div>
                    <h3 className="font-medium text-gray-900 group-hover:text-primary transition-colors">
                      {subItem.title}
                    </h3>
                    <p className="text-sm text-gray-500 mt-1">
                      {subItem.description}
                    </p>
                  </div>
                ))}
              </div>
            </div>
          )}
        </SheetContent>
      </Sheet>
    </>
  );
};

export default MobileMenu;
