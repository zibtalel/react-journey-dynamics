
import React from 'react';
import { Link } from 'react-router-dom';
import { Facebook, Instagram, Youtube, Linkedin, Mail, Phone, MapPin, ChevronRight } from 'lucide-react';

const Footer = () => {
  const currentYear = new Date().getFullYear();
  
  return (
    <footer className="bg-gray-900 text-white pt-16 pb-8">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mb-12">
          {/* Column 1: About Us */}
          <div className="space-y-4">
            <Link to="/" className="block mb-6">
              <img src="/logo.png" alt="ELLES" className="h-12 invert" />
            </Link>
            <p className="text-gray-300 text-sm leading-relaxed">
              ELLES est votre partenaire privilégié pour les vêtements de travail personnalisés.
              Nous offrons des solutions adaptées à chaque métier, alliant qualité, confort et style.
            </p>
            <div className="flex space-x-4 pt-4">
              <a href="#" className="bg-gray-800 p-2 rounded-full hover:bg-primary transition-colors duration-300">
                <Facebook className="h-4 w-4" />
              </a>
              <a href="#" className="bg-gray-800 p-2 rounded-full hover:bg-primary transition-colors duration-300">
                <Instagram className="h-4 w-4" />
              </a>
              <a href="#" className="bg-gray-800 p-2 rounded-full hover:bg-primary transition-colors duration-300">
                <Linkedin className="h-4 w-4" />
              </a>
              <a href="#" className="bg-gray-800 p-2 rounded-full hover:bg-primary transition-colors duration-300">
                <Youtube className="h-4 w-4" />
              </a>
            </div>
          </div>

          {/* Column 2: Quick Links */}
          <div className="space-y-4">
            <h4 className="font-semibold text-lg mb-6 relative after:content-[''] after:absolute after:w-12 after:h-1 after:bg-primary after:left-0 after:-bottom-2">
              Liens utiles
            </h4>
            <ul className="space-y-3">
              <li className="transition-transform hover:translate-x-1 duration-200">
                <Link to="/" className="text-gray-300 hover:text-white flex items-center">
                  <ChevronRight className="h-3 w-3 mr-2" />
                  <span>Accueil</span>
                </Link>
              </li>
              <li className="transition-transform hover:translate-x-1 duration-200">
                <Link to="/metiers" className="text-gray-300 hover:text-white flex items-center">
                  <ChevronRight className="h-3 w-3 mr-2" />
                  <span>Métiers</span>
                </Link>
              </li>
              <li className="transition-transform hover:translate-x-1 duration-200">
                <Link to="/personalization" className="text-gray-300 hover:text-white flex items-center">
                  <ChevronRight className="h-3 w-3 mr-2" />
                  <span>Personnalisation</span>
                </Link>
              </li>
              <li className="transition-transform hover:translate-x-1 duration-200">
                <Link to="/devis" className="text-gray-300 hover:text-white flex items-center">
                  <ChevronRight className="h-3 w-3 mr-2" />
                  <span>Demander un devis</span>
                </Link>
              </li>
              <li className="transition-transform hover:translate-x-1 duration-200">
                <Link to="/nos-packs" className="text-gray-300 hover:text-white flex items-center">
                  <ChevronRight className="h-3 w-3 mr-2" />
                  <span>Nos Packs</span>
                </Link>
              </li>
            </ul>
          </div>

          {/* Column 3: Contact (moved up from being column 4) */}
          <div className="space-y-4">
            <h4 className="font-semibold text-lg mb-6 relative after:content-[''] after:absolute after:w-12 after:h-1 after:bg-primary after:left-0 after:-bottom-2">
              Contact
            </h4>
            <ul className="space-y-4">
              <li className="flex items-start">
                <MapPin className="h-5 w-5 text-primary mr-3 mt-0.5" />
                <span className="text-gray-300">123 Rue du Commerce, 75001 Paris, France</span>
              </li>
              <li className="flex items-center">
                <Phone className="h-5 w-5 text-primary mr-3" />
                <span className="text-gray-300">01 23 45 67 89</span>
              </li>
              <li className="flex items-center">
                <Mail className="h-5 w-5 text-primary mr-3" />
                <span className="text-gray-300">contact@elles.com</span>
              </li>
            </ul>
            <div className="pt-4">
              <h5 className="text-sm font-semibold mb-3">Horaires d'ouverture:</h5>
              <p className="text-gray-300 text-sm">
                Lun - Ven: 9h00 - 18h00<br />
                Sam: 10h00 - 16h00<br />
                Dim: Fermé
              </p>
            </div>
          </div>
        </div>

        {/* Bottom Bar with additional links and copyright */}
        <div className="pt-8 border-t border-gray-800">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4 items-center">
            <div className="text-center md:text-left">
              <p className="text-gray-400 text-sm">
                © {currentYear} ELLES. Tous droits réservés.
              </p>
            </div>
            <div className="flex flex-wrap justify-center md:justify-end gap-x-4 gap-y-2">
              <Link to="/terms" className="text-gray-400 hover:text-white text-sm">Conditions d'utilisation</Link>
              <Link to="/privacy" className="text-gray-400 hover:text-white text-sm">Politique de confidentialité</Link>
              <Link to="/faq" className="text-gray-400 hover:text-white text-sm">FAQ</Link>
              <Link to="/livraison" className="text-gray-400 hover:text-white text-sm">Livraison</Link>
              <Link to="/retours" className="text-gray-400 hover:text-white text-sm">Retours</Link>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
