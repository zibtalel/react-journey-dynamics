import { motion } from 'framer-motion';
import { ArrowRight, X, Palette, Layout, Search, Globe, Smartphone, Code, BrainCircuit, BarChart3, Target, Users, PenTool } from 'lucide-react';
import { useState } from 'react';
import { Dialog, DialogContent } from "../components/ui/dialog";
import { Link } from 'react-router-dom';
import VideoBackground from '../components/VideoBackground';

const featureDetails = {
  seo: {
    title: "Référencement (SEO)",
    description: "Nous optimisons votre visibilité sur les moteurs de recherche pour atteindre votre audience.",
    features: [
      "Analyse de mots-clés",
      "Optimisation technique",
      "Création de contenu adapté",
      "Suivi des performances"
    ]
  },
  dev: {
    title: "Développement Web",
    description: "Création de sites web adaptés à vos besoins.",
    features: [
      "Sites sur mesure",
      "Interfaces adaptatives",
      "Solutions e-commerce",
      "Maintenance régulière"
    ]
  },
  design: {
    title: "Design Graphique",
    description: "Création d'identités visuelles qui reflètent votre vision.",
    features: [
      "Logos et chartes graphiques",
      "Supports de communication",
      "Design d'interfaces",
      "Illustrations"
    ]
  }
};

const services = [
  {
    title: "Sites Web & Applications",
    description: "Création de sites et d'applications adaptés à vos besoins",
    features: [
      "Sites vitrines",
      "E-commerce",
      "Applications web",
      "Applications mobiles",
      "Maintenance"
    ],
    icon: Globe
  },
  {
    title: "Marketing Digital",
    description: "Stratégies marketing pour développer votre présence en ligne",
    features: [
      "Réseaux sociaux",
      "SEO",
      "Publicité en ligne",
      "E-mail marketing",
      "Analytics"
    ],
    icon: Target
  }
];

const Digital = () => {
  const [open, setOpen] = useState(false);
  const [selectedFeature, setSelectedFeature] = useState(null);

  const digitalMarketingServices = [
    {
      title: "SEO (Référencement Naturel)",
      description: "Améliorez votre visibilité sur Google et attirez plus de clients.",
      icon: Search,
      details: featureDetails.seo
    },
    {
      title: "Développement Web",
      description: "Créez un site web sur mesure qui répond à vos besoins.",
      icon: Code,
      details: featureDetails.dev
    },
    {
      title: "Design Graphique",
      description: "Mettez en valeur votre identité visuelle avec un design unique.",
      icon: Palette,
      details: featureDetails.design
    },
    {
      title: "Branding",
      description: "Développez une image de marque forte et cohérente.",
      icon: PenTool,
    },
    {
      title: "Social Media Marketing",
      description: "Engagez votre audience et développez votre communauté sur les réseaux sociaux.",
      icon: Users,
    },
    {
      title: "Web Analytics",
      description: "Mesurez et analysez les performances de votre site web pour optimiser votre stratégie.",
      icon: BarChart3,
    },
  ];

  return (
    <div className="bg-black text-white min-h-screen pt-20">
      <VideoBackground videoUrl="/digitalbg.mp4" overlay="bg-black/50">
        <div className="container mx-auto text-center py-32 px-4 relative z-10">
          <motion.h1
            className="text-5xl md:text-6xl font-bold mb-8"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
          >
            Solutions Digitales
          </motion.h1>
          <motion.p
            className="text-xl md:text-2xl text-white/80 mb-12"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
          >
            Boostez votre présence en ligne avec nos services sur mesure
          </motion.p>
          <motion.div
            className="flex justify-center gap-4"
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.4 }}
          >
            <a
              href="#services"
              className="bg-gold-600 text-black font-semibold py-3 px-6 rounded-full hover:bg-gold-500 transition-colors"
            >
              Nos Services
            </a>
            <a
              href="#contact"
              className="border border-white text-white font-semibold py-3 px-6 rounded-full hover:bg-white/10 transition-colors"
            >
              Contactez-nous
            </a>
          </motion.div>
        </div>
      </VideoBackground>

      <section id="services" className="py-20 px-4">
        <div className="container mx-auto">
          <motion.div
            className="text-center mb-16"
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
          >
            <h2 className="text-4xl font-bold mb-4">Nos Services</h2>
            <p className="text-xl text-white/80">
              Des solutions complètes pour votre transformation digitale
            </p>
          </motion.div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {digitalMarketingServices.map((service, index) => (
              <motion.div
                key={index}
                className="relative group luxury-card p-6 rounded-xl border border-gold-600/20 hover:border-gold-600/40 transition-colors duration-300"
                initial={{ opacity: 0, y: 50 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: index * 0.2 }}
                onClick={() => {
                  setSelectedFeature(service.details);
                  setOpen(true);
                }}
              >
                <div className="absolute -inset-1 bg-gradient-to-r from-gold-600 to-gold-400 rounded-lg blur opacity-0 group-hover:opacity-25 transition duration-1000 group-hover:duration-200" />
                <div className="relative">
                  <div className="text-gold-400 mb-3">
                    <service.icon className="w-8 h-8" />
                  </div>
                  <h3 className="text-xl font-semibold mb-2">{service.title}</h3>
                  <p className="text-white/70">{service.description}</p>
                </div>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      <section id="technologies" className="py-20 px-4 bg-rich-black">
        <div className="container mx-auto">
          <motion.div
            className="text-center mb-16"
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
          >
            <h2 className="text-4xl font-bold mb-4">
              Technologies que nous utilisons
            </h2>
            <p className="text-xl text-white/80">
              Un arsenal technologique pour des solutions performantes
            </p>
          </motion.div>

          <div className="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-6 gap-8">
            {[
              "React",
              "Node.js",
              "Next.js",
              "Tailwind CSS",
              "GraphQL",
              "PostgreSQL",
            ].map((tech, index) => (
              <motion.div
                key={index}
                className="bg-black/30 rounded-lg p-4 flex items-center justify-center text-white/80 text-lg font-medium"
                initial={{ opacity: 0, scale: 0.8 }}
                whileInView={{ opacity: 1, scale: 1 }}
                viewport={{ once: true }}
                transition={{ delay: index * 0.1 }}
              >
                {tech}
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      <section id="contact" className="py-20 px-4">
        <div className="container mx-auto text-center">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="max-w-3xl mx-auto"
          >
            <h2 className="text-4xl font-bold mb-4">
              Prêt à transformer votre présence digitale ?
            </h2>
            <p className="text-xl text-white/80 mb-8">
              Contactez-nous pour discuter de vos projets et découvrir comment
              nous pouvons vous aider à atteindre vos objectifs.
            </p>
            <a
              href="/contact"
              className="bg-gold-600 text-black font-semibold py-4 px-8 rounded-full hover:bg-gold-500 transition-colors"
            >
              Contactez-nous
            </a>
          </motion.div>
        </div>
      </section>

      <Dialog open={open} onOpenChange={setOpen}>
        <DialogContent className="bg-rich-black text-white rounded-lg p-8 max-w-md w-full">
          {selectedFeature && (
            <div>
              <div className="flex justify-between items-center mb-4">
                <h2 className="text-2xl font-bold">{selectedFeature.title}</h2>
                <button
                  onClick={() => setOpen(false)}
                  className="text-white hover:text-gray-300 transition-colors"
                >
                  <X className="w-6 h-6" />
                </button>
              </div>
              <p className="text-white/80 mb-4">{selectedFeature.description}</p>
              {selectedFeature.features && (
                <div>
                  <h3 className="text-xl font-semibold mb-2">Ce que nous offrons:</h3>
                  <ul className="list-disc list-inside text-white/70">
                    {selectedFeature.features.map((feature, index) => (
                      <li key={index}>{feature}</li>
                    ))}
                  </ul>
                </div>
              )}
            </div>
          )}
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default Digital;
