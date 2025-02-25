
import { useState } from 'react';
import { PreloadingScreen } from './components/PreloadingScreen';
import { ClientTypeModal } from './components/ClientTypeModal';
import Navbar from './components/Navbar';
import { Footer } from './components/Footer';
import { Contact } from './pages/Contact';
import Home from './pages/Home';
import type { ClientType } from './types';

function App() {
  const [isLoading, setIsLoading] = useState(true);
  const [clientType, setClientType] = useState<ClientType>(null);
  const [currentPage, setCurrentPage] = useState('home');

  // Loading screen effect
  useState(() => {
    const timer = setTimeout(() => {
      setIsLoading(false);
    }, 2000);

    return () => clearTimeout(timer);
  });

  if (isLoading) {
    return <PreloadingScreen />;
  }

  const renderPage = () => {
    switch (currentPage) {
      case 'contact':
        return <Contact />;
      default:
        return <Home />;
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
