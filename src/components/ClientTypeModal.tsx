
import type { ClientType } from '../types';
import { Building2, Users } from 'lucide-react';

interface ClientTypeModalProps {
  onSelect: (type: ClientType) => void;
}

const ClientTypeModal = ({ onSelect }: ClientTypeModalProps) => {
  return (
    <div className="fixed inset-0 bg-black/50 backdrop-blur-sm z-40 flex items-center justify-center p-4 animate-[fadeIn_0.3s_ease-in]">
      <div className="bg-white rounded-2xl p-8 max-w-2xl w-full shadow-2xl animate-[slideIn_0.5s_ease-out]">
        <div className="text-center mb-8">
          <h2 className="text-3xl font-playfair mb-3 bg-gradient-to-r from-[#96cc39] to-[#64381b] bg-clip-text text-transparent">Bienvenue chez Premium Dates</h2>
          <p className="text-gray-600">Veuillez sélectionner votre type de compte pour continuer</p>
        </div>
        
        <div className="grid md:grid-cols-2 gap-6">
          <button
            onClick={() => onSelect('B2B')}
            className="group relative overflow-hidden rounded-xl bg-white p-6 border-2 border-[#96cc39]/20 hover:border-[#96cc39] transition-all duration-300 hover:shadow-lg"
          >
            <div className="absolute inset-0 bg-[#96cc39]/5 transform scale-x-0 group-hover:scale-x-100 transition-transform origin-left duration-300 ease-out" />
            <div className="relative">
              <div className="w-16 h-16 bg-[#96cc39]/10 rounded-full flex items-center justify-center mb-4 group-hover:scale-110 transition-transform duration-300">
                <Building2 size={32} className="text-[#96cc39]" />
              </div>
              <h3 className="text-xl font-playfair mb-2 text-gray-900">Client Professionnel (B2B)</h3>
              <p className="text-gray-600 text-sm">
                Pour la distribution, le commerce de gros et les partenariats commerciaux
              </p>
            </div>
          </button>

          <button
            onClick={() => onSelect('B2C')}
            className="group relative overflow-hidden rounded-xl bg-white p-6 border-2 border-[#64381b]/20 hover:border-[#64381b] transition-all duration-300 hover:shadow-lg"
          >
            <div className="absolute inset-0 bg-[#64381b]/5 transform scale-x-0 group-hover:scale-x-100 transition-transform origin-left duration-300 ease-out" />
            <div className="relative">
              <div className="w-16 h-16 bg-[#64381b]/10 rounded-full flex items-center justify-center mb-4 group-hover:scale-110 transition-transform duration-300">
                <Users size={32} className="text-[#64381b]" />
              </div>
              <h3 className="text-xl font-playfair mb-2 text-gray-900">Client Particulier (B2C)</h3>
              <p className="text-gray-600 text-sm">
                Pour les achats personnels et les clients au détail
              </p>
            </div>
          </button>
        </div>

        <p className="text-center text-gray-500 text-sm mt-8">
          Votre sélection nous aidera à vous offrir l'expérience la plus adaptée
        </p>
      </div>
    </div>
  );
};

export { ClientTypeModal };
