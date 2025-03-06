
import React from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";

interface FaqModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const FaqModal: React.FC<FaqModalProps> = ({ isOpen, onClose }) => {
  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-2xl">
        <DialogHeader>
          <DialogTitle className="text-center font-serif text-3xl font-bold text-primary">
            Questions Fréquentes
          </DialogTitle>
        </DialogHeader>
        <div className="mt-4 max-h-[70vh] overflow-y-auto">
          <Accordion type="single" collapsible>
            <AccordionItem value="item-1">
              <AccordionTrigger>Quels sont les délais de livraison ?</AccordionTrigger>
              <AccordionContent>
                Nous livrons en France métropolitaine sous 2-4 jours ouvrés. Pour les commandes internationales, comptez 5-7 jours ouvrés.
              </AccordionContent>
            </AccordionItem>
            <AccordionItem value="item-2">
              <AccordionTrigger>Comment puis-je retourner un article ?</AccordionTrigger>
              <AccordionContent>
                Vous disposez de 30 jours pour retourner un article. Il doit être non porté et dans son emballage d'origine. Les frais de retour sont gratuits.
              </AccordionContent>
            </AccordionItem>
            <AccordionItem value="item-3">
              <AccordionTrigger>Proposez-vous des réductions pour les commandes en gros ?</AccordionTrigger>
              <AccordionContent>
                Oui, nous proposons des tarifs préférentiels pour les commandes professionnelles en grande quantité. Contactez notre service commercial pour plus d'informations.
              </AccordionContent>
            </AccordionItem>
            <AccordionItem value="item-4">
              <AccordionTrigger>Quels sont vos délais de personnalisation ?</AccordionTrigger>
              <AccordionContent>
                Les délais de personnalisation varient selon la complexité et le volume de votre commande. En règle générale, comptez 3-5 jours ouvrés supplémentaires pour les personnalisations standards, et 5-7 jours pour les personnalisations complexes.
              </AccordionContent>
            </AccordionItem>
            <AccordionItem value="item-5">
              <AccordionTrigger>Puis-je recevoir des échantillons avant de passer commande ?</AccordionTrigger>
              <AccordionContent>
                Oui, nous proposons un service d'échantillonnage pour les commandes professionnelles. Contactez notre équipe commerciale pour discuter de vos besoins spécifiques et organiser l'envoi d'échantillons.
              </AccordionContent>
            </AccordionItem>
          </Accordion>
        </div>
      </DialogContent>
    </Dialog>
  );
};

export default FaqModal;
