
import React, { useState } from 'react';
import { Phone, MapPin, Mail, Clock, Facebook, Instagram, Youtube, HelpCircle, Info } from "lucide-react";
import { Link } from "react-router-dom";
import { cn } from "@/lib/utils";
import FaqModal from '../modals/FaqModal';
import AboutModal from '../modals/AboutModal';

const PromoBar: React.FC = () => {
  const [isFaqOpen, setIsFaqOpen] = useState(false);
  const [isAboutOpen, setIsAboutOpen] = useState(false);

  return (
    <>
      <div className="w-full bg-gradient-to-r from-[#333333] to-[#555555] text-white py-2.5">
        <div className="container mx-auto px-4 flex items-center justify-between text-sm">
          <div className="flex items-center gap-4">
            <a href="tel:+21671234567" className="flex items-center gap-1.5 hover:text-[#FFD700] transition-colors">
              <Phone className="h-3.5 w-3.5" />
              <span className="hidden sm:inline font-medium">+216 71 234 567</span>
            </a>
            <div className="hidden md:flex items-center gap-1.5">
              <MapPin className="h-3.5 w-3.5" />
              <span className="font-medium">123 Rue de Tunis, Tunis 1000</span>
            </div>
            <a href="mailto:contact@elles.com" className="hidden lg:flex items-center gap-1.5 hover:text-[#FFD700] transition-colors">
              <Mail className="h-3.5 w-3.5" />
              <span className="font-medium">contact@elles.com</span>
            </a>
            <div className="hidden xl:flex items-center gap-1.5">
              <Clock className="h-3.5 w-3.5" />
              <span className="font-medium">Lun-Ven: 9h-18h</span>
            </div>
          </div>
          
          <div className="flex items-center gap-4">
            <div className="hidden sm:flex items-center divide-x divide-gray-500">
              <button 
                onClick={() => setIsFaqOpen(true)} 
                className="pr-3 text-xs flex items-center gap-1 hover:text-[#FFD700] transition-colors"
              >
                <HelpCircle className="h-3.5 w-3.5" />
                <span>Aide</span>
              </button>
              <Link to="/devis" className="px-3 text-xs hover:text-[#FFD700] transition-colors">Contact</Link>
              <button 
                onClick={() => setIsAboutOpen(true)} 
                className="pl-3 text-xs flex items-center gap-1 hover:text-[#FFD700] transition-colors"
              >
                <Info className="h-3.5 w-3.5" />
                <span>À propos</span>
              </button>
            </div>
            <div className="flex items-center gap-3">
              <a 
                href="https://facebook.com" 
                target="_blank" 
                rel="noopener noreferrer" 
                aria-label="Facebook" 
                className="hover:text-[#FFD700] transition-colors"
              >
                <Facebook className="h-3.5 w-3.5" />
              </a>
              <a 
                href="https://instagram.com" 
                target="_blank" 
                rel="noopener noreferrer" 
                aria-label="Instagram" 
                className="hover:text-[#FFD700] transition-colors"
              >
                <Instagram className="h-3.5 w-3.5" />
              </a>
              <a 
                href="https://youtube.com" 
                target="_blank" 
                rel="noopener noreferrer" 
                aria-label="YouTube" 
                className="hover:text-[#FFD700] transition-colors"
              >
                <Youtube className="h-3.5 w-3.5" />
              </a>
            </div>
          </div>
        </div>
      </div>

      {/* FAQ Modal */}
      <FaqModal isOpen={isFaqOpen} onClose={() => setIsFaqOpen(false)} />
      
      {/* About Modal */}
      <AboutModal isOpen={isAboutOpen} onClose={() => setIsAboutOpen(false)} />
    </>
  );
};

export default PromoBar;
