
import React, { useState, useEffect } from 'react';
import { MessageSquare, Facebook, Instagram, Youtube } from "lucide-react";

const SocialButtons: React.FC = () => {
  const [isExpanded, setIsExpanded] = useState(true);

  useEffect(() => {
    // Auto-collapse the help button after 4 seconds
    const timer = setTimeout(() => {
      setIsExpanded(false);
    }, 4000);

    return () => clearTimeout(timer);
  }, []);

  return (
    <>
      <a
        href="https://wa.me/+33600000000"
        target="_blank"
        rel="noopener noreferrer"
        className={`fixed left-6 bottom-6 z-50 flex items-center ${
          isExpanded ? 'gap-2 px-4' : 'justify-center p-3'
        } bg-green-500 hover:bg-green-600 text-white py-3 rounded-full shadow-lg transition-all duration-300`}
        onMouseEnter={() => setIsExpanded(true)}
        onMouseLeave={() => setIsExpanded(false)}
      >
        <MessageSquare className="h-5 w-5" />
        {isExpanded && <span className="font-medium whitespace-nowrap">Besoin d'aide ?</span>}
      </a>

      <div className="fixed right-6 bottom-6 z-50 flex flex-col gap-3">
        <a
          href="https://facebook.com"
          target="_blank"
          rel="noopener noreferrer"
          className="bg-primary hover:bg-primary/90 text-white p-3 rounded-full shadow-lg transition-all duration-300 hover:scale-110"
          aria-label="Visit our Facebook page"
        >
          <Facebook className="h-5 w-5" />
        </a>
        <a
          href="https://instagram.com"
          target="_blank"
          rel="noopener noreferrer"
          className="bg-primary hover:bg-primary/90 text-white p-3 rounded-full shadow-lg transition-all duration-300 hover:scale-110"
          aria-label="Visit our Instagram page"
        >
          <Instagram className="h-5 w-5" />
        </a>
        <a
          href="https://youtube.com"
          target="_blank"
          rel="noopener noreferrer"
          className="bg-primary hover:bg-primary/90 text-white p-3 rounded-full shadow-lg transition-all duration-300 hover:scale-110"
          aria-label="Visit our YouTube channel"
        >
          <Youtube className="h-5 w-5" />
        </a>
      </div>
    </>
  );
};

export default SocialButtons;
